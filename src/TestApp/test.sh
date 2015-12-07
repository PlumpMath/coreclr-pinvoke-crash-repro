#!/bin/sh
g++ -c -fpic test.cpp 
g++ -shared test.o -o libtest.so
LD_LIBRARY_PATH=$PWD dnx run
