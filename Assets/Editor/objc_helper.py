import os, sys
import subprocess
import re
import os_helper
from subprocess import call

def add_method(file_path, method_lines):
  print "\nAdding method {0}".format(method_lines[0])
  os_helper.insert_before(file_path, "@end", method_lines)
  

def create_import(file_to_import, file_path, extension="mm"):
  print "\nImport {0} into {1}".format(file_to_import, file_path)
  insert_before_keyword	= "@implementation" if extension == "mm" else "@interface"
  os_helper.insert_before(file_path, insert_before_keyword, ['#import \"{0}\"'.format(file_to_import)])


def insert_into_didFinishLaunchingWithOptions(file_path, codeList):
	print "\nInsert {0} into applicationDidFinishLaunchingWithOptions".format(codeList)
	os_helper.insert_after_and_before(file_path, "didFinishLaunchingWithOptions:(NSDictionary*)launchOptions", "return NO;", codeList)


def add_into_classes(pbx_proj_path, pbx_build_file_code, pbx_file_reference_code, pbx_group_code, group_folder_reference, pbx_source_build_phase_code):
  os_helper.insert_after(pbx_proj_path, "/* Icon.png in Resources */ = {isa = PBXBuildFile; fileRef = ", pbx_build_file_code)
  os_helper.insert_after(pbx_proj_path, '/* Icon.png */ = {isa = PBXFileReference; lastKnownFileType = image.png; path = Icon.png; sourceTree = "<group>";', pbx_file_reference_code)
  os_helper.insert_before(pbx_proj_path, "/* UI */ = {", pbx_group_code)
  os_helper.insert_before(pbx_proj_path, "/* Unity */,", group_folder_reference)
  os_helper.insert_before(pbx_proj_path, "/* DisplayManager.mm in Sources */,", pbx_source_build_phase_code)


def add_framework(pbx_proj_path, pbx_build_file_code, pbx_file_reference_code, pbx_framework_code, pbx_group_code):
  framework_to_add = re.search("/\* (.*) in Frameworks \*/", pbx_build_file_code[0])
  if not framework_to_add:
    print "Adding Framework stopped because we didn't find the name in {0}".format(pbx_build_file_code[0])
    return
  
  framework_name = framework_to_add.group(1)
  print "\nAdding Framework {0}".format(framework_name)
  
  if(os_helper.contains(pbx_proj_path, framework_name)):
    print "{0} already included".format(framework_name)
    return
  
  os_helper.insert_after(pbx_proj_path, "/* Icon.png in Resources */ = {isa = PBXBuildFile; fileRef = ", pbx_build_file_code)
  os_helper.insert_after(pbx_proj_path, '/* Icon.png */ = {isa = PBXFileReference; lastKnownFileType = image.png; path = Icon.png; sourceTree = "<group>";', pbx_file_reference_code)
  os_helper.insert_before(pbx_proj_path, "/* Foundation.framework in Frameworks */,", pbx_framework_code)
  os_helper.insert_before(pbx_proj_path, "/* SystemConfiguration.framework */,", pbx_group_code)


def add_resource(pbx_proj_path, pbx_build_file_code, pbx_file_reference_code, pbx_group_code, pbx_resource_code):
  resource_to_add = re.search("/\* (.*) in Resources \*/", pbx_build_file_code[0])
  if not resource_to_add:
    print "Adding Resource stopped because we didn't find the name in {0}".format(pbx_build_file_code[0])
    return

  resource_name = resource_to_add.group(1)
  print "\nAdding Resource {0}".format(resource_name)

  if(os_helper.contains(pbx_proj_path, resource_name)):
    print "{0} already included".format(resource_name)
    return

  os_helper.insert_after(pbx_proj_path, "/* Icon.png in Resources */ = {isa = PBXBuildFile; fileRef = ", pbx_build_file_code)
  os_helper.insert_after(pbx_proj_path, '/* Icon.png */ = {isa = PBXFileReference; lastKnownFileType = image.png; path = Icon.png; sourceTree = "<group>";', pbx_file_reference_code)
  os_helper.insert_before(pbx_proj_path, "/* SystemConfiguration.framework */,", pbx_group_code)
  os_helper.insert_after_regex(pbx_proj_path, "\t*.* /\* Icon\.png in Resources \*/,", pbx_resource_code)


def set_flag(pbx_proj_path, flag, value):
	print "\nSetting {0} to {1}".format(flag, value)

	new_file						= pbx_proj_path + ".tmp"
	file 								= open(pbx_proj_path, 'r')
	tmp_file 						= open(new_file, 'w+')

	for line in file:
		if flag in line:
			line = re.sub(r'= [a-zA-Z]*', '= ' + value, line)
		tmp_file.write(line)

	file.close()
	tmp_file.close()

	call(["rm", pbx_proj_path])
	call(["mv", new_file, pbx_proj_path])
	print "{0} modified :]".format(flag)


def add_to_plist(plist_path, code):
  print "\nAdding code to plist"
  os_helper.insert_before(plist_path, "</dict>", code)
  
  print "Code Added :]"


def add_url_scheme(plist_path, urlScheme):
  print "\nAdding urlScheme {0}".format(urlScheme)
  
  if not os_helper.contains(plist_path, "CFBundleURLTypes"):
    create_url_scheme(plist_path)

  if os_helper.contains(plist_path, urlScheme):
    print "URL Scheme already present!"
    return
    
  os_helper.insert_after_and_before(plist_path, "<key>CFBundleURLSchemes</key>", "</array>", ["<string>{0}</string>".format(urlScheme)])

  print "URL Scheme Added :]"


def create_url_scheme(plist_path):
  print "\nCreating URL Scheme in {0}".format(plist_path)
  
  add_to_plist(plist_path, ["<key>CFBundleURLTypes</key>",
  "<array>",
  "<dict>",
  "<key>CFBundleURLSchemes</key>",
  "<array>",
  "</array>",
  "</dict>",
  "</array>"])