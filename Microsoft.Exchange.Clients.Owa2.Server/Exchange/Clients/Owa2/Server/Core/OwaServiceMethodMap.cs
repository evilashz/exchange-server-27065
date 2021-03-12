using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000253 RID: 595
	internal class OwaServiceMethodMap
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x00052830 File Offset: 0x00050A30
		internal OwaServiceMethodMap(Type contractType)
		{
			this.methodMap = OwaServiceMethodMap.Load(contractType);
			this.supportedMethods = OwaServiceMethodMap.LoadMethodSetFromWebConfig("OWAHttpHandlerMethods");
			this.unsupportedMethods = OwaServiceMethodMap.LoadMethodSetFromWebConfig("OWAHttpHandlerUnsupportedMethods");
			this.supportAllMethods = BaseApplication.GetAppSetting<bool>("OWAHttpHandlerSupportAllMethods", false);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00052880 File Offset: 0x00050A80
		internal bool TryGetMethodInfo(string methodName, out ServiceMethodInfo methodInfo)
		{
			if ((this.supportAllMethods || this.supportedMethods.Contains(methodName)) && !this.unsupportedMethods.Contains(methodName))
			{
				return this.methodMap.TryGetValue(methodName, out methodInfo);
			}
			methodInfo = null;
			return false;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000528B8 File Offset: 0x00050AB8
		internal static TResult HandleAsync<TResult>(Task<TResult> task)
		{
			return task.Result;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000528DC File Offset: 0x00050ADC
		private static Dictionary<string, ServiceMethodInfo> Load(Type contractType)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Dictionary<string, List<Attribute>> dictionary = OwaServiceMethodMap.CollectCustomAttributes(contractType);
			MethodInfo[] methods = contractType.GetMethods();
			IEnumerable<MethodInfo> source = from method in methods
			where method.Name.StartsWith("End")
			select method;
			Dictionary<string, MethodInfo> endMethodMap = source.ToDictionary((MethodInfo methodInfo) => methodInfo.Name);
			Dictionary<string, ServiceMethodInfo> dictionary2 = new Dictionary<string, ServiceMethodInfo>();
			foreach (MethodInfo methodInfo2 in methods)
			{
				List<Attribute> attributes;
				if (dictionary.TryGetValue(methodInfo2.Name, out attributes))
				{
					OwaServiceMethodMap.ProcessMethod(methodInfo2, endMethodMap, dictionary2, attributes);
				}
			}
			stopwatch.Stop();
			ExTraceGlobals.CoreTracer.TraceDebug<long, string>(0L, "OwaServiceMethodMap.Load took {0} milliseconds to load methods for contract type {1}", stopwatch.ElapsedMilliseconds, contractType.Name);
			return dictionary2;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000529B4 File Offset: 0x00050BB4
		private static HashSet<string> LoadMethodSetFromWebConfig(string settingKey)
		{
			string appSetting = BaseApplication.GetAppSetting<string>(settingKey, string.Empty);
			HashSet<string> hashSet = new HashSet<string>();
			if (!string.IsNullOrWhiteSpace(appSetting))
			{
				foreach (string text in appSetting.Split(new char[]
				{
					','
				}))
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						hashSet.Add(text.Trim());
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00052A20 File Offset: 0x00050C20
		private static Dictionary<string, List<Attribute>> CollectCustomAttributes(Type contractType)
		{
			Dictionary<string, List<Attribute>> dictionary = new Dictionary<string, List<Attribute>>();
			foreach (Type type in contractType.GetInterfaces())
			{
				OwaServiceMethodMap.CollectCustomAttributesForType(dictionary, type);
			}
			OwaServiceMethodMap.CollectCustomAttributesForType(dictionary, contractType);
			return dictionary;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00052A5C File Offset: 0x00050C5C
		private static void CollectCustomAttributesForType(Dictionary<string, List<Attribute>> dict, Type type)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			foreach (MethodInfo methodInfo in type.GetMethods(bindingAttr))
			{
				foreach (Attribute item in methodInfo.GetCustomAttributes())
				{
					List<Attribute> list;
					if (dict.TryGetValue(methodInfo.Name, out list))
					{
						list.Add(item);
					}
					else
					{
						list = new List<Attribute>();
						list.Add(item);
						dict.Add(methodInfo.Name, list);
					}
				}
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00052B00 File Offset: 0x00050D00
		private static TAttribute GetCustomAttribute<TAttribute>(List<Attribute> attributes) where TAttribute : Attribute
		{
			if (attributes == null)
			{
				return default(TAttribute);
			}
			return attributes.OfType<TAttribute>().FirstOrDefault<TAttribute>();
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00052B28 File Offset: 0x00050D28
		private static void ProcessMethod(MethodInfo methodInfo, Dictionary<string, MethodInfo> endMethodMap, Dictionary<string, ServiceMethodInfo> methodTable, List<Attribute> attributes)
		{
			OperationContractAttribute customAttribute = OwaServiceMethodMap.GetCustomAttribute<OperationContractAttribute>(attributes);
			if (customAttribute == null)
			{
				return;
			}
			WebInvokeAttribute customAttribute2 = OwaServiceMethodMap.GetCustomAttribute<WebInvokeAttribute>(attributes);
			JsonRequestFormatAttribute customAttribute3 = OwaServiceMethodMap.GetCustomAttribute<JsonRequestFormatAttribute>(attributes);
			WebGetAttribute customAttribute4 = OwaServiceMethodMap.GetCustomAttribute<WebGetAttribute>(attributes);
			OperationBehaviorAttribute customAttribute5 = OwaServiceMethodMap.GetCustomAttribute<OperationBehaviorAttribute>(attributes);
			JsonResponseOptionsAttribute customAttribute6 = OwaServiceMethodMap.GetCustomAttribute<JsonResponseOptionsAttribute>(attributes);
			JsonRequestWrapperTypeAttribute customAttribute7 = OwaServiceMethodMap.GetCustomAttribute<JsonRequestWrapperTypeAttribute>(attributes);
			AsyncStateMachineAttribute customAttribute8 = OwaServiceMethodMap.GetCustomAttribute<AsyncStateMachineAttribute>(attributes);
			bool flag = customAttribute != null && customAttribute.AsyncPattern;
			bool flag2 = customAttribute8 != null;
			bool flag3 = customAttribute5 == null || customAttribute5.AutoDisposeParameters;
			bool isResponseCacheable = customAttribute6 != null && customAttribute6.IsCacheable;
			WebMessageBodyStyle webMessageBodyStyle = (customAttribute2 != null) ? customAttribute2.BodyStyle : WebMessageBodyStyle.Bare;
			if (customAttribute2 != null)
			{
				WebMessageFormat requestFormat = customAttribute2.RequestFormat;
			}
			if (customAttribute2 != null)
			{
				WebMessageFormat responseFormat = customAttribute2.ResponseFormat;
			}
			JsonRequestFormat jsonRequestFormat = (customAttribute3 != null) ? customAttribute3.Format : JsonRequestFormat.Custom;
			bool isHttpGet = (customAttribute2 != null) ? customAttribute2.Method.Equals("GET", StringComparison.InvariantCultureIgnoreCase) : (customAttribute4 != null);
			string text = (customAttribute2 != null) ? customAttribute2.UriTemplate : ((customAttribute4 != null) ? customAttribute4.UriTemplate : null);
			UriTemplate uriTemplate = (!string.IsNullOrEmpty(text)) ? new UriTemplate(text) : null;
			bool flag4 = webMessageBodyStyle == WebMessageBodyStyle.WrappedRequest || webMessageBodyStyle == WebMessageBodyStyle.Wrapped;
			bool isWrappedResponse = webMessageBodyStyle == WebMessageBodyStyle.WrappedResponse || webMessageBodyStyle == WebMessageBodyStyle.Wrapped;
			WebMessageFormat webMethodRequestFormat = (customAttribute2 != null && customAttribute2.IsRequestFormatSetExplicitly) ? customAttribute2.RequestFormat : WebMessageFormat.Json;
			WebMessageFormat webMethodResponseFormat = (customAttribute2 != null && customAttribute2.IsResponseFormatSetExplicitly) ? customAttribute2.ResponseFormat : WebMessageFormat.Json;
			Type type = (customAttribute7 != null) ? customAttribute7.Type : null;
			string text2 = methodInfo.Name;
			MethodInfo beginMethod = null;
			MethodInfo methodInfo2 = null;
			MethodInfo syncMethod = null;
			MethodInfo genericAsyncTaskMethod = null;
			Type type2 = null;
			Type type3;
			if (text2.StartsWith("Begin", StringComparison.InvariantCultureIgnoreCase) && flag)
			{
				type3 = ((methodInfo.GetParameters().Length > 0) ? methodInfo.GetParameters()[0].ParameterType : null);
				beginMethod = methodInfo;
				text2 = text2.Substring("Begin".Length);
				string key = "End" + text2;
				if (endMethodMap.TryGetValue(key, out methodInfo2))
				{
					type2 = methodInfo2.ReturnType;
				}
			}
			else
			{
				syncMethod = methodInfo;
				type3 = ((methodInfo.GetParameters().Length > 0) ? methodInfo.GetParameters()[0].ParameterType : null);
				type2 = methodInfo.ReturnType;
				if (flag2 && type2 != null && type2.GenericTypeArguments != null && type2.GenericTypeArguments.Length > 0)
				{
					genericAsyncTaskMethod = OwaServiceMethodMap.handleAsyncMethodInfo.MakeGenericMethod(type2.GenericTypeArguments);
					type2 = type2.GenericTypeArguments[0];
				}
			}
			bool isStreamedResponse = OwaServiceMethodMap.IsStreamResponse(type2);
			bool shouldAutoDisposeResponse = flag3 && OwaServiceMethodMap.ImplementsInterface<IDisposable>(type2);
			bool shouldAutoDisposeRequest = flag3 && OwaServiceMethodMap.ImplementsInterface<IDisposable>(type3);
			if (flag4 && type == null)
			{
				string wrappedRequestTypeName = OwaServiceMethodMap.GetWrappedRequestTypeName(text2);
				type = OwaServiceMethodMap.thisAssembly.GetType(wrappedRequestTypeName, false);
			}
			ServiceMethodInfo value = new ServiceMethodInfo
			{
				BeginMethod = beginMethod,
				EndMethod = methodInfo2,
				GenericAsyncTaskMethod = genericAsyncTaskMethod,
				IsAsyncAwait = flag2,
				IsAsyncPattern = flag,
				IsHttpGet = isHttpGet,
				IsResponseCacheable = isResponseCacheable,
				IsStreamedResponse = isStreamedResponse,
				IsWrappedRequest = flag4,
				IsWrappedResponse = isWrappedResponse,
				JsonRequestFormat = jsonRequestFormat,
				Name = text2,
				RequestType = type3,
				ResponseType = type2,
				ShouldAutoDisposeRequest = shouldAutoDisposeRequest,
				ShouldAutoDisposeResponse = shouldAutoDisposeResponse,
				SyncMethod = syncMethod,
				UriTemplate = uriTemplate,
				WebMethodRequestFormat = webMethodRequestFormat,
				WebMethodResponseFormat = webMethodResponseFormat,
				WrappedRequestType = type,
				WrappedRequestTypeParameterMap = OwaServiceMethodMap.BuildParameterMap(type)
			};
			methodTable.Add(text2, value);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00052EA1 File Offset: 0x000510A1
		private static string GetWrappedRequestTypeName(string methodName)
		{
			return string.Format("{0}.{1}RequestWrapper", "Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers", methodName);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00052EC5 File Offset: 0x000510C5
		private static bool ImplementsInterface<TInterface>(Type type)
		{
			return type != null && type.GetInterfaces().Any((Type itype) => itype == typeof(TInterface));
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00052EEC File Offset: 0x000510EC
		private static bool IsStreamResponse(Type responseType)
		{
			return responseType != null && (responseType == typeof(Stream) || responseType.IsSubclassOf(typeof(Stream)));
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00052F2C File Offset: 0x0005112C
		private static Dictionary<string, string> BuildParameterMap(Type type)
		{
			if (type == null)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				string name = propertyInfo.Name;
				DataMemberAttribute customAttribute = propertyInfo.GetCustomAttribute<DataMemberAttribute>();
				if (customAttribute != null && !string.IsNullOrEmpty(customAttribute.Name))
				{
					name = customAttribute.Name;
				}
				dictionary.Add(name, propertyInfo.Name);
			}
			return dictionary;
		}

		// Token: 0x04000C5A RID: 3162
		private const string supportedMethodsKey = "OWAHttpHandlerMethods";

		// Token: 0x04000C5B RID: 3163
		private const string unsupportedMethodsKey = "OWAHttpHandlerUnsupportedMethods";

		// Token: 0x04000C5C RID: 3164
		private const string supportAllMethodsKey = "OWAHttpHandlerSupportAllMethods";

		// Token: 0x04000C5D RID: 3165
		private const string AsyncMethodBeginPrefix = "Begin";

		// Token: 0x04000C5E RID: 3166
		private const string AsyncMethodEndPrefix = "End";

		// Token: 0x04000C5F RID: 3167
		private const string WrappedRequestNamespace = "Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers";

		// Token: 0x04000C60 RID: 3168
		private static MethodInfo handleAsyncMethodInfo = typeof(OwaServiceMethodMap).GetMethod("HandleAsync", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000C61 RID: 3169
		private static Assembly thisAssembly = typeof(OwaServiceMethodMap).Assembly;

		// Token: 0x04000C62 RID: 3170
		private HashSet<string> unsupportedMethods;

		// Token: 0x04000C63 RID: 3171
		private readonly bool supportAllMethods;

		// Token: 0x04000C64 RID: 3172
		private HashSet<string> supportedMethods;

		// Token: 0x04000C65 RID: 3173
		private Dictionary<string, ServiceMethodInfo> methodMap;
	}
}
