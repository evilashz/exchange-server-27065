using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D8 RID: 472
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MwiTargetException : MwiDeliveryException
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0003645D File Offset: 0x0003465D
		public MwiTargetException(string targetName, int responseCode, string responseText) : base(Strings.UMRpcError(targetName, responseCode, responseText))
		{
			this.targetName = targetName;
			this.responseCode = responseCode;
			this.responseText = responseText;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00036482 File Offset: 0x00034682
		public MwiTargetException(string targetName, int responseCode, string responseText, Exception innerException) : base(Strings.UMRpcError(targetName, responseCode, responseText), innerException)
		{
			this.targetName = targetName;
			this.responseCode = responseCode;
			this.responseText = responseText;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x000364AC File Offset: 0x000346AC
		protected MwiTargetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.targetName = (string)info.GetValue("targetName", typeof(string));
			this.responseCode = (int)info.GetValue("responseCode", typeof(int));
			this.responseText = (string)info.GetValue("responseText", typeof(string));
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00036521 File Offset: 0x00034721
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("targetName", this.targetName);
			info.AddValue("responseCode", this.responseCode);
			info.AddValue("responseText", this.responseText);
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003655E File Offset: 0x0003475E
		public string TargetName
		{
			get
			{
				return this.targetName;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00036566 File Offset: 0x00034766
		public int ResponseCode
		{
			get
			{
				return this.responseCode;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003656E File Offset: 0x0003476E
		public string ResponseText
		{
			get
			{
				return this.responseText;
			}
		}

		// Token: 0x040007A7 RID: 1959
		private readonly string targetName;

		// Token: 0x040007A8 RID: 1960
		private readonly int responseCode;

		// Token: 0x040007A9 RID: 1961
		private readonly string responseText;
	}
}
