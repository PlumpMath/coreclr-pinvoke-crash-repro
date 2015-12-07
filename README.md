A minimal test case (that should be a close approximation to how SWIG generates its wrapper code for exception handling with proxy class support)

This works as expected on Windows (the exception is caught on the native side, essential information passed to over to the .net side where an equivalent exception proxy class is throw with the transferred information)

On Linux (CoreCLR v1.0.0 x64 rc1-update1 on Ubunut 14.04 64-bit) the method that constructs the .net proxy exception class is called, but crashes out with SIGABRT

Running (Linux)
===============

Requires: g++, CoreCLR v1.0.0 x64 rc1-update1

 1. dnvm use the above required runtime
 2. Run test.sh

Running (Windows)
=================

Requires: CoreCLR v1.0.0 x64 rc1-update1, MSVC 2015 (The compiler I used for this test case)

 1. Run x64 tools command prompt
 2. dnvm use the above required runtime
 3. Run test.cmd