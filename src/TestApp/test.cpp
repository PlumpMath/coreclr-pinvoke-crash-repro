#include <cstdio>

#ifdef _WIN32
#define API_EXPORT __declspec(dllexport)
#define API_STDCALL __stdcall
#else
#define API_EXPORT
#define API_STDCALL
#endif

class MyException
{
public:
	MyException() {}
	~MyException() {}
	const char* msg() const { return "Something went wrong"; }
};

typedef void (*dotnet_exception_callback_t)(const char*);
static dotnet_exception_callback_t custom_exceptions_callback;

extern "C" API_EXPORT void API_STDCALL registerExceptionCallback(dotnet_exception_callback_t callback)
{
	custom_exceptions_callback = callback;
}

extern "C" API_EXPORT int API_STDCALL foo()
{
	int result = -1;
	try
	{
		throw new MyException();
		result = 3;
	}
	catch (MyException* ex)
	{
		if (NULL != custom_exceptions_callback) {
			result = 1;
	                custom_exceptions_callback(ex->msg());
		} else {
			result = 2;
		}
		delete ex;
	}
	return result;
}
