using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001181 RID: 4481
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidVersionException : LocalizedException
	{
		// Token: 0x0600B674 RID: 46708 RVA: 0x0029FE29 File Offset: 0x0029E029
		public InvalidVersionException(string version, string format) : base(Strings.InvalidVersion(version, format))
		{
			this.version = version;
			this.format = format;
		}

		// Token: 0x0600B675 RID: 46709 RVA: 0x0029FE46 File Offset: 0x0029E046
		public InvalidVersionException(string version, string format, Exception innerException) : base(Strings.InvalidVersion(version, format), innerException)
		{
			this.version = version;
			this.format = format;
		}

		// Token: 0x0600B676 RID: 46710 RVA: 0x0029FE64 File Offset: 0x0029E064
		protected InvalidVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.version = (string)info.GetValue("version", typeof(string));
			this.format = (string)info.GetValue("format", typeof(string));
		}

		// Token: 0x0600B677 RID: 46711 RVA: 0x0029FEB9 File Offset: 0x0029E0B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("version", this.version);
			info.AddValue("format", this.format);
		}

		// Token: 0x1700398D RID: 14733
		// (get) Token: 0x0600B678 RID: 46712 RVA: 0x0029FEE5 File Offset: 0x0029E0E5
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700398E RID: 14734
		// (get) Token: 0x0600B679 RID: 46713 RVA: 0x0029FEED File Offset: 0x0029E0ED
		public string Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040062F3 RID: 25331
		private readonly string version;

		// Token: 0x040062F4 RID: 25332
		private readonly string format;
	}
}
