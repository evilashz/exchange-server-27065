using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005AC RID: 1452
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WatermarksBehindException : LocalizedException
	{
		// Token: 0x060026F5 RID: 9973 RVA: 0x000DE3AF File Offset: 0x000DC5AF
		public WatermarksBehindException(string database) : base(Strings.WatermarksBehind(database))
		{
			this.database = database;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000DE3C4 File Offset: 0x000DC5C4
		public WatermarksBehindException(string database, Exception innerException) : base(Strings.WatermarksBehind(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000DE3DA File Offset: 0x000DC5DA
		protected WatermarksBehindException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000DE404 File Offset: 0x000DC604
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x000DE41F File Offset: 0x000DC61F
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x04001C81 RID: 7297
		private readonly string database;
	}
}
