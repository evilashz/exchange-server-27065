using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020000CE RID: 206
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidRmsUrlException : LocalizedException
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00013D3A File Offset: 0x00011F3A
		public InvalidRmsUrlException(string s) : base(DrmStrings.InvalidRmsUrl(s))
		{
			this.s = s;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00013D4F File Offset: 0x00011F4F
		public InvalidRmsUrlException(string s, Exception innerException) : base(DrmStrings.InvalidRmsUrl(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00013D65 File Offset: 0x00011F65
		protected InvalidRmsUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00013D8F File Offset: 0x00011F8F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00013DAA File Offset: 0x00011FAA
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04000461 RID: 1121
		private readonly string s;
	}
}
