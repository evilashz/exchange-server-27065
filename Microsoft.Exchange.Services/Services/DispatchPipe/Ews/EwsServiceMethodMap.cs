using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Services.DispatchPipe.Base;

namespace Microsoft.Exchange.Services.DispatchPipe.Ews
{
	// Token: 0x02000DD4 RID: 3540
	internal sealed class EwsServiceMethodMap : ServiceMethodMapBase
	{
		// Token: 0x06005A78 RID: 23160 RVA: 0x0011A4DE File Offset: 0x001186DE
		internal EwsServiceMethodMap(Type contractType) : base(contractType)
		{
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x0011A4E8 File Offset: 0x001186E8
		internal bool Contains(string methodName)
		{
			ServiceMethodInfo serviceMethodInfo = null;
			return this.TryGetMethodInfo(methodName, out serviceMethodInfo);
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x0011A500 File Offset: 0x00118700
		internal override bool TryGetMethodInfo(string methodName, out ServiceMethodInfo methodInfo)
		{
			if (Global.HttpHandleDisabledMethods.Contains(methodName))
			{
				methodInfo = null;
				return false;
			}
			return base.TryGetMethodInfo(methodName, out methodInfo);
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x0011A51C File Offset: 0x0011871C
		protected override ServiceMethodInfo PostProcessMethod(ServiceMethodInfo methodInfo)
		{
			if (methodInfo == null || methodInfo.RequestType == null || methodInfo.ResponseType == null)
			{
				return null;
			}
			if (EwsServiceMethodMap.UnsupportedMethods.Contains(methodInfo.Name))
			{
				return null;
			}
			FieldInfo field = methodInfo.RequestType.GetField("Body");
			FieldInfo field2 = methodInfo.ResponseType.GetField("Body");
			if (field != null)
			{
				methodInfo.RequestBodyType = field.FieldType;
			}
			if (field2 != null)
			{
				methodInfo.WrappedResponseBodyField = field2;
				methodInfo.ResponseBodyType = field2.FieldType;
			}
			if (methodInfo.RequestBodyType != null && methodInfo.ResponseBodyType != null)
			{
				methodInfo.BeginMethod = methodInfo.RequestBodyType.GetMethod("Submit", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
				{
					methodInfo.ResponseBodyType
				});
				return methodInfo;
			}
			throw new NullReferenceException(string.Format("RequestBodyType and/or ResponseBodyType is null for method: {0}", methodInfo.Name));
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x0011A614 File Offset: 0x00118814
		public override Type GetWrappedRequestType(string methodName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040031FF RID: 12799
		private static HashSet<string> UnsupportedMethods = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"GetUserPhoto"
		};
	}
}
