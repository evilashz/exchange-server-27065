using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F6 RID: 502
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidResultTypeException : LocalizedException
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x00039071 File Offset: 0x00037271
		public InvalidResultTypeException(string resultType) : base(Strings.InvalidResultTypeException(resultType))
		{
			this.resultType = resultType;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00039086 File Offset: 0x00037286
		public InvalidResultTypeException(string resultType, Exception innerException) : base(Strings.InvalidResultTypeException(resultType), innerException)
		{
			this.resultType = resultType;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0003909C File Offset: 0x0003729C
		protected InvalidResultTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resultType = (string)info.GetValue("resultType", typeof(string));
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000390C6 File Offset: 0x000372C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resultType", this.resultType);
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x000390E1 File Offset: 0x000372E1
		public string ResultType
		{
			get
			{
				return this.resultType;
			}
		}

		// Token: 0x0400087C RID: 2172
		private readonly string resultType;
	}
}
