cl.exe /c /EHsc test.cpp
link.exe /dll /out:test.dll test.obj
dnx run