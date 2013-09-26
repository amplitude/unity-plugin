import os, sys
import subprocess
import re
from subprocess import call

def copy(currentLocation, newLocation):
  if not os.path.isdir(currentLocation) and not os.path.isfile(currentLocation):
    print "- {0} does not exist. Skipping".format(currentLocation)
    return

  print "\n- Copy {0} into {1}".format(currentLocation, newLocation)
  arguments = ["cp"]
  if os.path.isdir(currentLocation):
    arguments.append("-R")

  arguments.extend([currentLocation, newLocation])
  call(arguments)


def mkdir(newDir):
  print "\n- Creating dir {0}".format(newDir)
  arguments = ["mkdir", "-p", newDir]
  call(arguments)


def move(currentLocation, newLocation):
  if not os.path.isdir(currentLocation) and not os.path.isfile(currentLocation):
    print "- {0} does not exist. Skipping".format(currentLocation)
    return

  print "\n- Move {0} into {1}".format(currentLocation, newLocation)
  arguments = ["mv", currentLocation, newLocation]
  call(arguments)


def replace(file_path, lookup_string, new_string, only_first_ocurrence = True):
  print "- Replacing {0} for {1} into {2}".format(lookup_string, new_string, file_path)
  
  if not os.path.isfile(file_path):
    print "- Can't replace {0} on {1}. This is not a file".format(lookup_string, file_path)
    return

  new_file  = file_path + ".tmp"
  file      = open(file_path, 'r')
  tmp_file  = open(new_file, 'w+')
  found     = False

  for line in file:
    if lookup_string in line and not found:
      line = line.replace(lookup_string, new_string)
      if only_first_ocurrence:
        found = True
    tmp_file.write(line)

  file.close()
  tmp_file.close()

  call(["rm", file_path])
  call(["mv", new_file, file_path])
  print "- replacing done :]"


def contains(file_path, lookup_string):
  print "- Looking for {0} in {1}".format(lookup_string, file_path)

  if not os.path.isfile(file_path):
    print "- {0}: This is not a file".format(file_path)
    return

  file = open(file_path, 'r')

  for line in file:
    if lookup_string in line:
      print "- Found!"
      file.close()
      return True

  file.close()
  print "- No Luck"
  return False


def insert_after(file_path, after_code, codeList = []):
  new_file 							= file_path + ".tmp"
  code_already_exists 	= False
  code_done							= False
  write_code            = False
  file 									= open(file_path, 'r')
  tmp_file 							= open(new_file, 'w+')

  for line in file:
    if write_code and not code_done:
      if  codeList[0] not in line:
        for line_of_code in codeList:
          tmp_file.write(line_of_code + "\n")
      else:
        code_already_exists = True
      code_done = True
    elif codeList[0] in line:
      code_already_exists = True
      break
    elif after_code in line and not write_code and not code_done:
      write_code = True
    tmp_file.write(line)
  
  #In case it's the end of file A but we haven't written code into the file
  if write_code and not code_done:
    for line_of_code in codeList:
      tmp_file.write(line_of_code + "\n")

  file.close()
  tmp_file.close()

  if code_already_exists:
    print "- insert after code was already imported"
    call(["rm", new_file])
    return
  
  call(["rm", file_path])
  call(["mv", new_file, file_path])
  print "- insert after done :]"


def insert_after_regex(file_path, after_regex, codeList = []):
  new_file 							= file_path + ".tmp"
  code_already_exists 	= False
  code_done							= False
  write_code            = False
  file 									= open(file_path, 'r')
  tmp_file 							= open(new_file, 'w+')

  for line in file:
    if write_code and not code_done:
      if  codeList[0] not in line:
        for line_of_code in codeList:
          tmp_file.write(line_of_code + "\n")
      else:
        code_already_exists = True
      code_done = True
    elif codeList[0] in line:
      code_already_exists = True
      break
    else:
      regex_result = re.search(after_regex, line)
      if regex_result and not write_code and not code_done:
        write_code = True
    tmp_file.write(line)

  #In case it's the end of file A but we haven't written code into the file
  if write_code and not code_done:
    for line_of_code in codeList:
      tmp_file.write(line_of_code + "\n")

  file.close()
  tmp_file.close()

  if code_already_exists:
    print "- insert after regex was already imported"
    call(["rm", new_file])
    return

  call(["rm", file_path])
  call(["mv", new_file, file_path])
  print "- insert after regex done :]"


def insert_before(file_path, before_code, codeList = []):
  new_file 							= file_path + ".tmp"
  code_already_exists 	= False
  code_done							= False
  file 									= open(file_path, 'r')
  tmp_file 							= open(new_file, 'w+')

  for line in file:
    if codeList[0] in line:
      code_already_exists = True
      break
    elif before_code in line and not code_done:
      for line_of_code in codeList:
        tmp_file.write(line_of_code + "\n")
      code_done = True
    tmp_file.write(line)

  file.close()
  tmp_file.close()

  if code_already_exists:
    print "- insert before code was already imported"
    call(["rm", new_file])
    return

  call(["rm", file_path])
  call(["mv", new_file, file_path])
  print "- insert before done :]"


def insert_after_and_before(file_path, after_code, before_code, codeList=[]):
	print "- insert after {0} and before {1}".format(after_code, before_code)
	
	if len(codeList) <= 0:
		print "- Won't insert empty code into file"
		return
  
	new_file						= file_path + ".tmp"
	code_already_exists	= False
	code_done 					= False
	file 								= open(file_path, 'r')
	tmp_file 						= open(new_file, 'w+')

	states				= [after_code, before_code]
	state_index 	= 0
	current_state	= states[state_index]

	for line in file:
		if current_state == states[0]:
			if after_code in line:
				state_index		+= 1
				current_state	= states[state_index]
		elif current_state == states[1]:
			if codeList[0] in line:
				code_already_exists = True
				break
			if before_code in line and not code_done:
				for line_of_code in codeList:
					tmp_file.write(line_of_code + "\n")
				code_done = True
		tmp_file.write(line)

	file.close()
	tmp_file.close()

	if code_already_exists:
		print "- Code was already there"
		call(["rm", new_file])
		return
  
	call(["rm", file_path])
	call(["mv", new_file, file_path])
	print "- code inserted :]"


def get_text_after_and_before(file_path, reg_exp, before_code):
	print "- Get lines after RegExp {0} and before {1}".format(reg_exp, before_code)

	file	= open(file_path, 'r')
	text	= []
	regex	= False

	for line in file:
		if not regex:
			found_regex = re.search(reg_exp, line)
			if found_regex:
				regex = True
		else:
			if before_code in line:
				break
			else:
			  text.append(line)

	file.close()

	print "- Found {0} lines between text".format(len(text))
	return text
