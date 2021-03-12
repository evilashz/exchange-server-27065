using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000252 RID: 594
	internal class OwaServiceMethodDispatcher
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x00051AA9 File Offset: 0x0004FCA9
		internal OwaServiceMethodDispatcher(IOwaServiceMessageInspector inspector)
		{
			this.inspector = inspector;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00051AC0 File Offset: 0x0004FCC0
		private static Dictionary<string, object> ConvertRequestToParameterDictionary(ServiceMethodInfo methodInfo, IEnumerable<ParameterInfo> parameters, Stream requestStream)
		{
			Type wrappedRequestType = methodInfo.WrappedRequestType;
			if (wrappedRequestType != null)
			{
				OwaServiceMethodDispatcher.CreateJsonSerializer(wrappedRequestType);
				object wrapperObject = OwaServiceMethodDispatcher.ReadJsonObject(wrappedRequestType, requestStream);
				return OwaServiceMethodDispatcher.ConvertWrappedObjectToParameterDictionary(parameters, methodInfo.WrappedRequestTypeParameterMap, wrapperObject);
			}
			IEnumerable<Type> enumerable;
			if (parameters == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = parameters.ToList<ParameterInfo>().ConvertAll<Type>((ParameterInfo p) => p.ParameterType);
			}
			IEnumerable<Type> knownTypes = enumerable;
			DataContractJsonSerializer dataContractJsonSerializer = OwaServiceMethodDispatcher.CreateSimpleDictionaryJsonSerializer(knownTypes);
			return (Dictionary<string, object>)dataContractJsonSerializer.ReadObject(requestStream);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00051B3C File Offset: 0x0004FD3C
		private static Dictionary<string, object> ConvertWrappedObjectToParameterDictionary(IEnumerable<ParameterInfo> parameters, Dictionary<string, string> parameterMap, object wrapperObject)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Type type = wrapperObject.GetType();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				string name = parameterInfo.Name;
				parameterMap.TryGetValue(parameterInfo.Name, out name);
				object obj = null;
				PropertyInfo property = type.GetProperty(name);
				if (property != null)
				{
					obj = property.GetValue(wrapperObject);
				}
				if (obj != null)
				{
					dictionary.Add(parameterInfo.Name, obj);
				}
			}
			return dictionary;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00051BD8 File Offset: 0x0004FDD8
		private static void WriteResponse(ServiceMethodInfo methodInfo, HttpResponse httpResponse, object response)
		{
			if (response != null)
			{
				if (methodInfo.IsStreamedResponse)
				{
					httpResponse.Buffer = false;
					Stream stream = response as Stream;
					stream.CopyTo(httpResponse.OutputStream);
					httpResponse.OutputStream.Flush();
					return;
				}
				if (methodInfo.IsWrappedResponse)
				{
					Dictionary<string, object> graph = new Dictionary<string, object>
					{
						{
							methodInfo.Name + "Result",
							response
						}
					};
					DataContractJsonSerializer dataContractJsonSerializer = OwaServiceMethodDispatcher.CreateSimpleDictionaryJsonSerializer(new Type[]
					{
						response.GetType()
					});
					dataContractJsonSerializer.WriteObject(httpResponse.OutputStream, graph);
					return;
				}
				DataContractJsonSerializer dataContractJsonSerializer2 = OwaServiceMethodDispatcher.CreateJsonSerializer(methodInfo.ResponseType);
				dataContractJsonSerializer2.WriteObject(httpResponse.OutputStream, response);
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00051C84 File Offset: 0x0004FE84
		private static object ConvertStringToParameterValue(string strValue, ParameterInfo parameter)
		{
			try
			{
				if (parameter.ParameterType == typeof(string))
				{
					return strValue;
				}
				if (parameter.ParameterType.IsEnum)
				{
					return Enum.Parse(parameter.ParameterType, strValue);
				}
				if (parameter.ParameterType.IsValueType)
				{
					return Convert.ChangeType(strValue, parameter.ParameterType);
				}
				string s = "\"" + strValue + "\"";
				using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
				{
					return OwaServiceMethodDispatcher.ReadJsonObject(parameter.ParameterType, memoryStream);
				}
			}
			catch (InvalidCastException arg)
			{
				ExTraceGlobals.CoreTracer.TraceError<string, InvalidCastException>(0L, "Cast error occurred while converting string value to parameter {0}. Exception: {1}", parameter.Name, arg);
			}
			catch (FormatException arg2)
			{
				ExTraceGlobals.CoreTracer.TraceError<string, FormatException>(0L, "Format error occurred while converting string value to parameter {0}. Exception: {1}", parameter.Name, arg2);
			}
			catch (OverflowException arg3)
			{
				ExTraceGlobals.CoreTracer.TraceError<string, OverflowException>(0L, "Overflow error occurred while converting string value to parameter {0}. Exception: {1}", parameter.Name, arg3);
			}
			return null;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00051DB8 File Offset: 0x0004FFB8
		private static object ReadJsonObject(Type objectType, Stream stream)
		{
			object result;
			try
			{
				DataContractJsonSerializer dataContractJsonSerializer = OwaServiceMethodDispatcher.CreateJsonSerializer(objectType);
				result = dataContractJsonSerializer.ReadObject(stream);
			}
			catch (SerializationException ex)
			{
				string arg = OwaServiceMethodDispatcher.TryGetJsonContentFromStream(stream, 2048);
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("OwaServiceMethodDispatcher", null, "ReadJsonObject", string.Format("Type: {0} Exception: {1}, JSON: {2}", objectType.Name, ex.Message, arg)));
				throw new OwaSerializationException(string.Format("Cannot deserialize object of type {0}", objectType.Name), ex);
			}
			return result;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00051E38 File Offset: 0x00050038
		private static string TryGetJsonContentFromStream(Stream stream, int maxContentLength)
		{
			string result = string.Empty;
			try
			{
				if (stream.CanSeek)
				{
					stream.Position = 0L;
					using (StreamReader streamReader = new StreamReader(stream))
					{
						char[] array = new char[maxContentLength];
						int length = streamReader.Read(array, 0, array.Length);
						result = new string(array, 0, length);
					}
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.CoreTracer.TraceError<Exception>(0L, "Could not retrieve JSON content from stream for diagnostics. Exception: {0}", arg);
			}
			return result;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00051EC0 File Offset: 0x000500C0
		private static object InvokeMethod(HttpRequest request, MethodInfo methodInfo, object obj, params object[] parameters)
		{
			object result;
			try
			{
				result = methodInfo.Invoke(obj, parameters);
			}
			catch (ArgumentException ex)
			{
				string arg = OwaServiceMethodDispatcher.TryGetJsonContentFromStream(request.InputStream, 2048);
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("OwaServiceMethodDispatcher", null, "InvokeMethod", string.Format("Method: {0} Exception: {1}, JSON: {2}", methodInfo.Name, ex.Message, arg)));
				throw new OwaMethodArgumentException(string.Format("Invalid argument used to call method {0}", methodInfo.Name), ex);
			}
			return result;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00051F40 File Offset: 0x00050140
		private static object[] CreateMethodArgumentsFromUri(ServiceMethodInfo methodInfo, HttpRequest httpRequest)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (methodInfo.UriTemplate != null)
			{
				Uri baseAddress = new Uri(httpRequest.Url, httpRequest.Path);
				Uri url = httpRequest.Url;
				UriTemplateMatch uriTemplateMatch = methodInfo.UriTemplate.Match(baseAddress, url);
				foreach (string text in uriTemplateMatch.BoundVariables.AllKeys)
				{
					dictionary.Add(text, uriTemplateMatch.BoundVariables[text]);
				}
			}
			else
			{
				foreach (string text2 in httpRequest.QueryString.AllKeys)
				{
					dictionary.Add(text2, httpRequest.QueryString[text2]);
				}
			}
			MethodInfo methodInfo2 = methodInfo.IsAsyncPattern ? methodInfo.BeginMethod : methodInfo.SyncMethod;
			ParameterInfo[] parameters = methodInfo2.GetParameters();
			int num = parameters.Length;
			if (methodInfo.IsAsyncPattern)
			{
				num -= 2;
			}
			object[] array = (num > 0) ? new object[num] : null;
			for (int k = 0; k < num; k++)
			{
				object obj = null;
				if (dictionary != null)
				{
					ParameterInfo parameterInfo = parameters[k];
					string text3 = null;
					if (dictionary.TryGetValue(parameterInfo.Name, out text3) && !string.IsNullOrEmpty(text3))
					{
						obj = OwaServiceMethodDispatcher.ConvertStringToParameterValue(text3, parameterInfo);
					}
				}
				array[k] = obj;
			}
			return array;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00052094 File Offset: 0x00050294
		private static object[] CreateMethodArgumentsFromWrappedRequest(ServiceMethodInfo methodInfo, HttpRequest httpRequest)
		{
			ParameterInfo[] parameters = methodInfo.SyncMethod.GetParameters();
			Dictionary<string, object> dictionary = OwaServiceMethodDispatcher.ConvertRequestToParameterDictionary(methodInfo, parameters, httpRequest.InputStream);
			object[] array = (parameters.Length > 0) ? new object[parameters.Length] : null;
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				object obj = null;
				dictionary.TryGetValue(parameterInfo.Name, out obj);
				if (obj is object[] && parameterInfo.ParameterType.IsArray)
				{
					obj = OwaServiceMethodDispatcher.ConvertObjectArrayToTypedArray(obj, parameterInfo.ParameterType);
				}
				array[i] = obj;
			}
			return array;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00052120 File Offset: 0x00050320
		private static object ConvertObjectArrayToTypedArray(object value, Type arrayType)
		{
			object[] array = value as object[];
			Type elementType = arrayType.GetElementType();
			if (array != null)
			{
				Array array2 = Array.CreateInstance(elementType, array.Length);
				array.CopyTo(array2, 0);
				value = array2;
			}
			return value;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00052154 File Offset: 0x00050354
		private static object[] CreateMethodArgumentsFromRequest(ServiceMethodInfo methodInfo, HttpRequest httpRequest)
		{
			object[] result = null;
			if (methodInfo.IsWrappedRequest)
			{
				result = OwaServiceMethodDispatcher.CreateMethodArgumentsFromWrappedRequest(methodInfo, httpRequest);
			}
			else if (methodInfo.RequestType != null)
			{
				object obj = OwaServiceMethodDispatcher.ReadJsonObject(methodInfo.RequestType, httpRequest.InputStream);
				result = new object[]
				{
					obj
				};
			}
			return result;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000521A4 File Offset: 0x000503A4
		private static DataContractJsonSerializer CreateJsonSerializer(Type objectType)
		{
			DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings
			{
				MaxItemsInObjectGraph = int.MaxValue
			};
			return new DataContractJsonSerializer(objectType, settings);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000521CC File Offset: 0x000503CC
		private static DataContractJsonSerializer CreateSimpleDictionaryJsonSerializer(IEnumerable<Type> knownTypes)
		{
			DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings
			{
				MaxItemsInObjectGraph = int.MaxValue,
				UseSimpleDictionaryFormat = true,
				KnownTypes = knownTypes
			};
			return new DataContractJsonSerializer(typeof(Dictionary<string, object>), settings);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0005220C File Offset: 0x0005040C
		private static void DisposeObjects(params object[] objs)
		{
			foreach (object obj in objs)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00052270 File Offset: 0x00050470
		private void InternalInvokeMethod(ServiceMethodInfo methodInfo, object service, HttpRequest httpRequest, HttpResponse httpResponse, object[] arguments)
		{
			object request = (arguments != null) ? arguments[0] : null;
			this.inspector.AfterReceiveRequest(httpRequest, methodInfo.Name, request);
			if (methodInfo.ShouldAutoDisposeRequest && arguments != null)
			{
				this.delayedDisposalRequestObjects = arguments;
			}
			object response = null;
			using (CpuTracker.StartCpuTracking("EXEC"))
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					response = this.InvokeSyncMethod(httpRequest, methodInfo, service, arguments);
				}, new Func<Exception, bool>(this.CanIgnoreExceptionForWatsonReport));
			}
			if (methodInfo.ShouldAutoDisposeResponse && response != null)
			{
				this.delayedDisposalResponseObject = response;
			}
			using (CpuTracker.StartCpuTracking("WRITE"))
			{
				OwaServiceMethodDispatcher.WriteResponse(methodInfo, httpResponse, response);
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000523A8 File Offset: 0x000505A8
		private object InvokeSyncMethod(HttpRequest request, ServiceMethodInfo methodInfo, object service, object[] arguments)
		{
			if (methodInfo.IsAsyncAwait)
			{
				object obj = OwaServiceMethodDispatcher.InvokeMethod(request, methodInfo.SyncMethod, service, arguments);
				return methodInfo.GenericAsyncTaskMethod.Invoke(null, new object[]
				{
					obj
				});
			}
			return OwaServiceMethodDispatcher.InvokeMethod(request, methodInfo.SyncMethod, service, arguments);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000523F8 File Offset: 0x000505F8
		private bool CanIgnoreExceptionForWatsonReport(Exception exception)
		{
			if (OwaDiagnostics.CanIgnoreExceptionForWatsonReport(exception))
			{
				return true;
			}
			TargetInvocationException ex = exception as TargetInvocationException;
			return ex != null && ex.InnerException != null && OwaDiagnostics.CanIgnoreExceptionForWatsonReport(ex.InnerException);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00052494 File Offset: 0x00050694
		private IAsyncResult InternalInvokeBeginMethod(ServiceMethodInfo methodInfo, object service, HttpRequest httpRequest, AsyncCallback asyncCallback, object[] arguments)
		{
			int num = (arguments != null) ? arguments.Length : 0;
			object request = (num > 0) ? arguments[0] : null;
			this.inspector.AfterReceiveRequest(httpRequest, methodInfo.Name, request);
			if (methodInfo.ShouldAutoDisposeRequest && arguments != null)
			{
				this.delayedDisposalRequestObjects = arguments;
			}
			IAsyncResult result = null;
			using (CpuTracker.StartCpuTracking("BEGIN"))
			{
				object[] invokeArgs = this.ConstructAsyncInvokeArguments(arguments, asyncCallback);
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					result = (IAsyncResult)OwaServiceMethodDispatcher.InvokeMethod(httpRequest, methodInfo.BeginMethod, service, invokeArgs);
				}, new Func<Exception, bool>(this.CanIgnoreExceptionForWatsonReport));
			}
			return result;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0005257C File Offset: 0x0005077C
		private object[] ConstructAsyncInvokeArguments(object[] arguments, AsyncCallback asyncCallback)
		{
			object[] array2;
			if (arguments == null || arguments.Length == 0)
			{
				object[] array = new object[2];
				array[0] = asyncCallback;
				array2 = array;
			}
			else if (arguments.Length == 1)
			{
				object[] array3 = new object[3];
				array3[0] = arguments[0];
				array3[1] = asyncCallback;
				array2 = array3;
			}
			else
			{
				array2 = new object[arguments.Length + 2];
				arguments.CopyTo(array2, 0);
				array2[arguments.Length] = asyncCallback;
				array2[arguments.Length + 1] = null;
			}
			return array2;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000525E0 File Offset: 0x000507E0
		internal void InvokeMethod(ServiceMethodInfo methodInfo, object service, HttpRequest httpRequest, HttpResponse httpResponse)
		{
			object[] arguments = OwaServiceMethodDispatcher.CreateMethodArgumentsFromRequest(methodInfo, httpRequest);
			this.InternalInvokeMethod(methodInfo, service, httpRequest, httpResponse, arguments);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00052604 File Offset: 0x00050804
		internal void InvokeGetMethod(ServiceMethodInfo methodInfo, object service, HttpRequest httpRequest, HttpResponse httpResponse)
		{
			ExTraceGlobals.CoreTracer.TraceDebug(0L, "OwaServiceMethodDispatcher.InvokeGetMethod");
			object[] arguments = OwaServiceMethodDispatcher.CreateMethodArgumentsFromUri(methodInfo, httpRequest);
			this.InternalInvokeMethod(methodInfo, service, httpRequest, httpResponse, arguments);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00052638 File Offset: 0x00050838
		internal IAsyncResult InvokeBeginMethod(ServiceMethodInfo methodInfo, object service, HttpRequest httpRequest, AsyncCallback asyncCallback)
		{
			ExTraceGlobals.CoreTracer.TraceDebug(0L, "OwaServiceMethodDispatcher.InvokeBeginMethod");
			object[] arguments = OwaServiceMethodDispatcher.CreateMethodArgumentsFromRequest(methodInfo, httpRequest);
			return this.InternalInvokeBeginMethod(methodInfo, service, httpRequest, asyncCallback, arguments);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0005266C File Offset: 0x0005086C
		internal IAsyncResult InvokeBeginGetMethod(ServiceMethodInfo methodInfo, object service, HttpRequest httpRequest, AsyncCallback asyncCallback)
		{
			ExTraceGlobals.CoreTracer.TraceDebug(0L, "OwaServiceMethodDispatcher.InvokeBeginGetMethod");
			object[] arguments = OwaServiceMethodDispatcher.CreateMethodArgumentsFromUri(methodInfo, httpRequest);
			return this.InternalInvokeBeginMethod(methodInfo, service, httpRequest, asyncCallback, arguments);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000526E4 File Offset: 0x000508E4
		internal void InvokeEndMethod(ServiceMethodInfo methodInfo, object service, IAsyncResult result, HttpResponse httpResponse)
		{
			ExTraceGlobals.CoreTracer.TraceDebug(0L, "OwaServiceMethodDispatcher.InvokeEndMethod");
			object response = null;
			using (CpuTracker.StartCpuTracking("END"))
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					response = methodInfo.EndMethod.Invoke(service, new object[]
					{
						result
					});
				}, new Func<Exception, bool>(this.CanIgnoreExceptionForWatsonReport));
			}
			if (methodInfo.ShouldAutoDisposeResponse && response != null)
			{
				this.delayedDisposalResponseObject = response;
			}
			this.inspector.BeforeSendReply(httpResponse, methodInfo.Name, response);
			using (CpuTracker.StartCpuTracking("WRITE"))
			{
				OwaServiceMethodDispatcher.WriteResponse(methodInfo, httpResponse, response);
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000527E4 File Offset: 0x000509E4
		internal void DisposeParameters()
		{
			if (this.delayedDisposalRequestObjects != null)
			{
				OwaServiceMethodDispatcher.DisposeObjects(this.delayedDisposalRequestObjects);
				this.delayedDisposalRequestObjects = null;
			}
			if (this.delayedDisposalResponseObject != null)
			{
				OwaServiceMethodDispatcher.DisposeObjects(new object[]
				{
					this.delayedDisposalResponseObject
				});
				this.delayedDisposalResponseObject = null;
			}
		}

		// Token: 0x04000C55 RID: 3157
		private const int MaxJsonLoggingSize = 2048;

		// Token: 0x04000C56 RID: 3158
		private IOwaServiceMessageInspector inspector;

		// Token: 0x04000C57 RID: 3159
		private object[] delayedDisposalRequestObjects;

		// Token: 0x04000C58 RID: 3160
		private object delayedDisposalResponseObject;
	}
}
