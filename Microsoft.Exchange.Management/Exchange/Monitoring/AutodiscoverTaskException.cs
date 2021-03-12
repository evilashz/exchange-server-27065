using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001102 RID: 4354
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutodiscoverTaskException : LocalizedException
	{
		// Token: 0x0600B3F4 RID: 46068 RVA: 0x0029BF95 File Offset: 0x0029A195
		public AutodiscoverTaskException(string field, string autodiscoverData) : base(Strings.messageAutodiscoverTaskException(field, autodiscoverData))
		{
			this.field = field;
			this.autodiscoverData = autodiscoverData;
		}

		// Token: 0x0600B3F5 RID: 46069 RVA: 0x0029BFB2 File Offset: 0x0029A1B2
		public AutodiscoverTaskException(string field, string autodiscoverData, Exception innerException) : base(Strings.messageAutodiscoverTaskException(field, autodiscoverData), innerException)
		{
			this.field = field;
			this.autodiscoverData = autodiscoverData;
		}

		// Token: 0x0600B3F6 RID: 46070 RVA: 0x0029BFD0 File Offset: 0x0029A1D0
		protected AutodiscoverTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.field = (string)info.GetValue("field", typeof(string));
			this.autodiscoverData = (string)info.GetValue("autodiscoverData", typeof(string));
		}

		// Token: 0x0600B3F7 RID: 46071 RVA: 0x0029C025 File Offset: 0x0029A225
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("field", this.field);
			info.AddValue("autodiscoverData", this.autodiscoverData);
		}

		// Token: 0x17003909 RID: 14601
		// (get) Token: 0x0600B3F8 RID: 46072 RVA: 0x0029C051 File Offset: 0x0029A251
		public string Field
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x1700390A RID: 14602
		// (get) Token: 0x0600B3F9 RID: 46073 RVA: 0x0029C059 File Offset: 0x0029A259
		public string AutodiscoverData
		{
			get
			{
				return this.autodiscoverData;
			}
		}

		// Token: 0x0400626F RID: 25199
		private readonly string field;

		// Token: 0x04006270 RID: 25200
		private readonly string autodiscoverData;
	}
}
