using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F1 RID: 497
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class HeaderFileArgumentInvalidException : LocalizedException
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00038E19 File Offset: 0x00037019
		public HeaderFileArgumentInvalidException(string argName) : base(Strings.HeaderFileArgumentInvalid(argName))
		{
			this.argName = argName;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00038E2E File Offset: 0x0003702E
		public HeaderFileArgumentInvalidException(string argName, Exception innerException) : base(Strings.HeaderFileArgumentInvalid(argName), innerException)
		{
			this.argName = argName;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00038E44 File Offset: 0x00037044
		protected HeaderFileArgumentInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.argName = (string)info.GetValue("argName", typeof(string));
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00038E6E File Offset: 0x0003706E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("argName", this.argName);
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00038E89 File Offset: 0x00037089
		public string ArgName
		{
			get
			{
				return this.argName;
			}
		}

		// Token: 0x04000877 RID: 2167
		private readonly string argName;
	}
}
