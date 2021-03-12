using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001116 RID: 4374
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CutoverMigrationNotSupportedForProtocolException : LocalizedException
	{
		// Token: 0x0600B45D RID: 46173 RVA: 0x0029CAC9 File Offset: 0x0029ACC9
		public CutoverMigrationNotSupportedForProtocolException(string protocol) : base(Strings.CutoverMigrationNotSupportedForProtocol(protocol))
		{
			this.protocol = protocol;
		}

		// Token: 0x0600B45E RID: 46174 RVA: 0x0029CADE File Offset: 0x0029ACDE
		public CutoverMigrationNotSupportedForProtocolException(string protocol, Exception innerException) : base(Strings.CutoverMigrationNotSupportedForProtocol(protocol), innerException)
		{
			this.protocol = protocol;
		}

		// Token: 0x0600B45F RID: 46175 RVA: 0x0029CAF4 File Offset: 0x0029ACF4
		protected CutoverMigrationNotSupportedForProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.protocol = (string)info.GetValue("protocol", typeof(string));
		}

		// Token: 0x0600B460 RID: 46176 RVA: 0x0029CB1E File Offset: 0x0029AD1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("protocol", this.protocol);
		}

		// Token: 0x17003922 RID: 14626
		// (get) Token: 0x0600B461 RID: 46177 RVA: 0x0029CB39 File Offset: 0x0029AD39
		public string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x04006288 RID: 25224
		private readonly string protocol;
	}
}
