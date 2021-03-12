using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200001F RID: 31
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WMIErrorException : BitlockerUtilException
	{
		// Token: 0x060000AB RID: 171 RVA: 0x0000502E File Offset: 0x0000322E
		public WMIErrorException(int returnValue, string methodName, string errorMessage) : base(DiskManagementStrings.WMIError(returnValue, methodName, errorMessage))
		{
			this.returnValue = returnValue;
			this.methodName = methodName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005058 File Offset: 0x00003258
		public WMIErrorException(int returnValue, string methodName, string errorMessage, Exception innerException) : base(DiskManagementStrings.WMIError(returnValue, methodName, errorMessage), innerException)
		{
			this.returnValue = returnValue;
			this.methodName = methodName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005084 File Offset: 0x00003284
		protected WMIErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.returnValue = (int)info.GetValue("returnValue", typeof(int));
			this.methodName = (string)info.GetValue("methodName", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000050F9 File Offset: 0x000032F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("returnValue", this.returnValue);
			info.AddValue("methodName", this.methodName);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005136 File Offset: 0x00003336
		public int ReturnValue
		{
			get
			{
				return this.returnValue;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000513E File Offset: 0x0000333E
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005146 File Offset: 0x00003346
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04000065 RID: 101
		private readonly int returnValue;

		// Token: 0x04000066 RID: 102
		private readonly string methodName;

		// Token: 0x04000067 RID: 103
		private readonly string errorMessage;
	}
}
