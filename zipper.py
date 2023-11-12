#!/usr/bin/env python3

# MUST run this from project root because we use relative paths

# Author: tnn4
# Version: 0.2.0
# Created on: 2023-09-30
# License: MIT or Apache2.0

# system environemnt
import sys
import os
import argparse

# run commands
import subprocess
import shlex

# ZIP
from zipfile import ZipFile
zip_usage="""
# Create a ZipFile Object
with ZipFile('E:/Zipped file.zip', 'w') as zip_object:
   # Adding files that need to be zipped
   zip_object.write('E:/Folder to be zipped/Greetings.txt')
   zip_object.write('E:/Folder to be zipped/Introduction.txt')

# Check to see if the zip file is created
if os.path.exists('E:/Zipped file.zip'):
   print("ZIP file created")
else:
   print("ZIP file not created")
"""

# Example files to zip list
file_list2 = [
    "bin",
    "templates",
    "book.toml",
    "build.sh",
    "install-deps.sh",
    "LICENSE",
    "read.sh",
    "README.md",
    ".gitignore",
]
DESC="This python script packages your project into a zip archive.\nUse a files2zip.txt at the root of the directory to list files you want packaged."
FILES2ZIP="files2zip.txt"
DEFAULT_OUT="wiki_book.zip"

def help():
    print(f"You MUST run the script from your project root." )
    print(f"Usage: \n$ {sys.argv[0]} --input 'file1 file2 file3' --out my.zip\n" )
    print(f"Use {FILES2ZIP}: \n$ {sys.argv[0]} --input '{FILES2ZIP}' --out my.zip\n" )
    exit()
#end

def main():
    direct_file=False
    target_file=DEFAULT_OUT
    # BEGIN CLI parsing
    # accept files2zip.txt as first argument
    if len(sys.argv) == 2 and sys.argv[1] == FILES2ZIP:
        direct_file=True
        if sys.argv[1] == FILES2ZIP:
            print(f"Reading {FILES2ZIP}")
            # read file line by line into list
            file_list = []
            with open(FILES2ZIP) as file:
                for line in file:
                    file_list.append(line.rstrip())
                #loop
            #close
        else:
            #file_list = shlex.split(args.input)
            pass
        #fi
        
    #fi

    if not direct_file:
        parser = argparse.ArgumentParser(
        prog="zipper",
        description=DESC,
        )

        parser.add_argument('-i','--input',
                            help=f'input list of files to zip, if {FILES2ZIP} is given then it will be read line by line for files to zip')
        parser.add_argument('-o','--out',
                            help=f'output zip name, default={DEFAULT_OUT}')
        parser.add_argument('-h2', '--help2', action='store_true',
                            help='more help and exampes')

        args = parser.parse_args()



        # HELP!
        if args.help2 == True:
            help()
        #fi

        # Set default arguments
        if args.input == None:
            help()
            print("No files to zip. Exiting")
            return
        else:
            # if --input files2zip.txt
            if args.input == FILES2ZIP:
                print(f"Reading {FILES2ZIP}")
                # read file line by line into list
                file_list = []
                with open(FILES2ZIP) as file:
                    for line in file:
                        file_list.append(line.rstrip())
                    #loop
                #close
            else: # otherwise just take a list of files on the command line
                file_list = shlex.split(args.input)
            #fi
        #fi

        # Set default output
        if args.out == None:
            args.out = DEFAULT_OUT
            target_file=args.out
        else:
            target_file=args.out
        #fi
    #fi

    # END CLI parsing

    print(f"{sys.argv[0]}: packaging...")

    """
    # Get target file
    if len(sys.argv) > 1:
        target_file=sys.argv[0]
    elif len(sys.argv) == 1:
        help()
        target_file="out.zip"
    #fi
    """
    if os.path.exists(target_file):
        choice = input(f"ALERT! '{target_file}' already exists. Do you want to overwrite? (y/n)")
        if choice == "y" or choice == "y".upper():
            print("OK")
        else:
            return
        #fi
    #fi

    print(f"file_list={file_list}")

    # compress the file
    with ZipFile(target_file, "w") as zipper:
        for file in file_list:
            # zipper.write only writes single files so 
            # if file is a directory, we have to recursively walk it
            if os.path.isdir(file):
                # traverse root directory, and list directories as dirs and files as files
                print("\nFOR LOOP_1")
                for root, dirs, files in os.walk(file):
                    print(f"root={root}")
                    path = root.split(os.sep)
                    print(f">>PATH={path}")
                    print((len(path) - 1) * '---', os.path.basename(root))
                    
                    print("FOR LOOP_2")
                    for f in files:
                        print(len(path) * '---', f)
  
                        # trial code below
                        # use abs path -> ERR!, we're running from from project root not $HOME
                        # zipper.write(os.path.abspath(f)) # NOPE
                        
                        # use relative path -> ERR!
                        # zipper.write(os.path.relpath(f)) # NOPE
                        
                        # preserve the root -> OK!
                        processed_f=root+"/"+f
                        print(f"zipper_write= {processed_f}") # OK!
                        zipper.write(processed_f)
                    #loop
                #loop
            else:
                zipper.write(file)
            #fi
            
        #loop
        print(f"Zipped to: {target_file}")
    #close
#end


if __name__ == "__main__":
    sys.exit(main())
#fi
