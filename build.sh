#!/bin/bash


## FILE INFO ##
# Script:       build.sh
# Description:  build project and archive
# Author:       tnn4
# Generated on: ( 10/12/2023 )

me=$(basename $0)


## ARGUMENTS ##
# Process arguments/flags/switches here
process_args () {
    # if there are no arguments, do nothing
    if [[ $# -eq 0 ]];then
        help
        return
    fi
    args_no_last=${@:1:$#-1}
    
    for arg in $args_no_last; do
        # Process specific args here
        case $arg in
            *)
                echo "unknown arg"
                help_more
            ;;
        esac
    done
} 
# end process_args()


## HELP ##
# put help and usage instructions here
help_more() {
    # write more detailed usage instructions here
    echo "e.g. usage: $me --options"
}

help() {
    # write usage instructions here
    echo "e.g. usage: $me --options"
}

if [[ $1 == "-h" || $1 == "--help" ]];then
    help_more
    exit
fi

# END HELP

## MAIN ##

VERSION_FILE="src/v"
TMP="src/v1"
PROJ_NAME="test-gamepad"
# put main code here
main() {
    
    echo "[$me]: hello world!"
    process_args "$@"

    # keep track of version
    while IFS= read -r line; do
        ver=$line
    done < $VERSION_FILE
    touch $TMP
    echo "$((ver+1))" > $TMP
    cp $TMP $VERSION_FILE

    # uncomment to build for all desktop platforms
    # these should be valid RIDs
    #target_architectures=(osx-x64 linux-x64 win-x64)
    #for target in "${target_architectures[@]}"
    #do
    #    dotnet build -r "$target" --self-contained
    #done

    dotnet build && ./zipper.py -i files2zip.txt -o out/$PROJ_NAME-v"${ver}".zip && \
    echo "Packaged to out/$PROJ_NAME-v${ver}.zip"
}

# import functions only and don't execute if --source-only is present
if [ "${1}" = "--source-only" ];then
    echo 'Functions imported. Script not executed'
else
    main "$@"
fi

# END MAIN
