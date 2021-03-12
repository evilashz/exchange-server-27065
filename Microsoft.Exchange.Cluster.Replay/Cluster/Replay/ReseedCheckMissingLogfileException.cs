using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C5 RID: 1221
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReseedCheckMissingLogfileException : LocalizedException
	{
		// Token: 0x06002DB7 RID: 11703 RVA: 0x000C20B5 File Offset: 0x000C02B5
		public ReseedCheckMissingLogfileException(string logfile) : base(ReplayStrings.ReseedCheckMissingLogfile(logfile))
		{
			this.logfile = logfile;
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000C20CA File Offset: 0x000C02CA
		public ReseedCheckMissingLogfileException(string logfile, Exception innerException) : base(ReplayStrings.ReseedCheckMissingLogfile(logfile), innerException)
		{
			this.logfile = logfile;
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000C20E0 File Offset: 0x000C02E0
		protected ReseedCheckMissingLogfileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logfile = (string)info.GetValue("logfile", typeof(string));
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000C210A File Offset: 0x000C030A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logfile", this.logfile);
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000C2125 File Offset: 0x000C0325
		public string Logfile
		{
			get
			{
				return this.logfile;
			}
		}

		// Token: 0x0400154A RID: 5450
		private readonly string logfile;
	}
}
