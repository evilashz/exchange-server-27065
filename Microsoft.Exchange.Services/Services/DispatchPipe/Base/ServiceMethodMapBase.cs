using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.DispatchPipe.Base
{
	// Token: 0x02000DCE RID: 3534
	internal abstract class ServiceMethodMapBase
	{
		// Token: 0x06005A2C RID: 23084 RVA: 0x00118C2A File Offset: 0x00116E2A
		internal static TResult HandleAsync<TResult>(Task<TResult> task)
		{
			return task.Result;
		}

		// Token: 0x06005A2D RID: 23085
		public abstract Type GetWrappedRequestType(string methodName);

		// Token: 0x06005A2E RID: 23086 RVA: 0x00118C32 File Offset: 0x00116E32
		public ServiceMethodMapBase(Type contractType)
		{
			this.methodMap = this.Load(contractType);
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x00118C47 File Offset: 0x00116E47
		internal virtual bool TryGetMethodInfo(string methodName, out ServiceMethodInfo methodInfo)
		{
			return this.methodMap.TryGetValue(methodName, out methodInfo);
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x00118C56 File Offset: 0x00116E56
		protected virtual ServiceMethodInfo PostProcessMethod(ServiceMethodInfo methodInfo)
		{
			return methodInfo;
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x00118C74 File Offset: 0x00116E74
		private Dictionary<string, ServiceMethodInfo> Load(Type contractType)
		{
			Dictionary<string, List<Attribute>> dictionary = ServiceMethodMapBase.CollectCustomAttributes(contractType);
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
					ServiceMethodInfo serviceMethodInfo = this.PostProcessMethod(this.ProcessMethod(methodInfo2, endMethodMap, attributes));
					if (serviceMethodInfo != null)
					{
						dictionary2.Add(serviceMethodInfo.Name, serviceMethodInfo);
					}
				}
			}
			return dictionary2;
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x00118D38 File Offset: 0x00116F38
		private static Dictionary<string, List<Attribute>> CollectCustomAttributes(Type contractType)
		{
			Dictionary<string, List<Attribute>> dictionary = new Dictionary<string, List<Attribute>>();
			foreach (Type type in contractType.GetInterfaces())
			{
				ServiceMethodMapBase.CollectCustomAttributesForType(dictionary, type);
			}
			ServiceMethodMapBase.CollectCustomAttributesForType(dictionary, contractType);
			return dictionary;
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x00118D74 File Offset: 0x00116F74
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

		// Token: 0x06005A34 RID: 23092 RVA: 0x00118E18 File Offset: 0x00117018
		private ServiceMethodInfo ProcessMethod(MethodInfo methodInfo, Dictionary<string, MethodInfo> endMethodMap, List<Attribute> attributes)
		{
			OperationContractAttribute customAttribute = ServiceMethodMapBase.GetCustomAttribute<OperationContractAttribute>(attributes);
			if (customAttribute == null)
			{
				return null;
			}
			WebInvokeAttribute customAttribute2 = ServiceMethodMapBase.GetCustomAttribute<WebInvokeAttribute>(attributes);
			JsonRequestFormatAttribute customAttribute3 = ServiceMethodMapBase.GetCustomAttribute<JsonRequestFormatAttribute>(attributes);
			WebGetAttribute customAttribute4 = ServiceMethodMapBase.GetCustomAttribute<WebGetAttribute>(attributes);
			OperationBehaviorAttribute customAttribute5 = ServiceMethodMapBase.GetCustomAttribute<OperationBehaviorAttribute>(attributes);
			JsonResponseOptionsAttribute customAttribute6 = ServiceMethodMapBase.GetCustomAttribute<JsonResponseOptionsAttribute>(attributes);
			JsonRequestWrapperTypeAttribute customAttribute7 = ServiceMethodMapBase.GetCustomAttribute<JsonRequestWrapperTypeAttribute>(attributes);
			AsyncStateMachineAttribute customAttribute8 = ServiceMethodMapBase.GetCustomAttribute<AsyncStateMachineAttribute>(attributes);
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
					genericAsyncTaskMethod = ServiceMethodMapBase.handleAsyncMethodInfo.MakeGenericMethod(type2.GenericTypeArguments);
					type2 = type2.GenericTypeArguments[0];
				}
			}
			bool isStreamedResponse = ServiceMethodMapBase.IsStreamResponse(type2);
			bool shouldAutoDisposeResponse = flag3 && ServiceMethodMapBase.ImplementsInterface<IDisposable>(type2);
			bool shouldAutoDisposeRequest = flag3 && ServiceMethodMapBase.ImplementsInterface<IDisposable>(type3);
			if (flag4 && type == null)
			{
				type = this.GetWrappedRequestType(text2);
			}
			Type requestBodyType = (type3.GetField("Body") != null) ? type3.GetField("Body").FieldType : null;
			Type responseBodyType = (type2.GetField("Body") != null) ? type2.GetField("Body").FieldType : null;
			return new ServiceMethodInfo
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
				RequestBodyType = requestBodyType,
				ResponseType = type2,
				ResponseBodyType = responseBodyType,
				ShouldAutoDisposeRequest = shouldAutoDisposeRequest,
				ShouldAutoDisposeResponse = shouldAutoDisposeResponse,
				SyncMethod = syncMethod,
				UriTemplate = uriTemplate,
				WebMethodRequestFormat = webMethodRequestFormat,
				WebMethodResponseFormat = webMethodResponseFormat,
				WrappedRequestType = type,
				WrappedRequestTypeParameterMap = ServiceMethodMapBase.BuildParameterMap(type)
			};
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x001191E4 File Offset: 0x001173E4
		private static TAttribute GetCustomAttribute<TAttribute>(List<Attribute> attributes) where TAttribute : Attribute
		{
			if (attributes == null)
			{
				return default(TAttribute);
			}
			return attributes.OfType<TAttribute>().FirstOrDefault<TAttribute>();
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x0011920C File Offset: 0x0011740C
		private static bool IsStreamResponse(Type responseType)
		{
			return responseType != null && (responseType == typeof(Stream) || responseType.IsSubclassOf(typeof(Stream)));
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0011925E File Offset: 0x0011745E
		private static bool ImplementsInterface<TInterface>(Type type)
		{
			return type != null && type.GetInterfaces().Any((Type itype) => itype == typeof(TInterface));
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x00119284 File Offset: 0x00117484
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

		// Token: 0x040031DD RID: 12765
		private const string AsyncMethodBeginPrefix = "Begin";

		// Token: 0x040031DE RID: 12766
		private const string AsyncMethodEndPrefix = "End";

		// Token: 0x040031DF RID: 12767
		private static MethodInfo handleAsyncMethodInfo = typeof(ServiceMethodMapBase).GetMethod("HandleAsync", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040031E0 RID: 12768
		private Dictionary<string, ServiceMethodInfo> methodMap;
	}
}
