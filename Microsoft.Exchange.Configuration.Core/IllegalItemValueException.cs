using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Core.LocStrings;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000033 RID: 51
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IllegalItemValueException : WinRMDataExchangeException
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0000758D File Offset: 0x0000578D
		public IllegalItemValueException(string value) : base(Strings.IllegalItemValue(value))
		{
			this.value = value;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000075A2 File Offset: 0x000057A2
		public IllegalItemValueException(string value, Exception innerException) : base(Strings.IllegalItemValue(value), innerException)
		{
			this.value = value;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000075B8 File Offset: 0x000057B8
		protected IllegalItemValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000075E2 File Offset: 0x000057E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("value", this.value);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000075FD File Offset: 0x000057FD
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040000C9 RID: 201
		private readonly string value;
	}
}
