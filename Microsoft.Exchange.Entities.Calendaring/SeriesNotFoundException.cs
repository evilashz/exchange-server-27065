using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000007 RID: 7
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SeriesNotFoundException : ObjectNotFoundException
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000028A8 File Offset: 0x00000AA8
		public SeriesNotFoundException(string seriesId) : base(CalendaringStrings.SeriesNotFound(seriesId))
		{
			this.seriesId = seriesId;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028BD File Offset: 0x00000ABD
		public SeriesNotFoundException(string seriesId, Exception innerException) : base(CalendaringStrings.SeriesNotFound(seriesId), innerException)
		{
			this.seriesId = seriesId;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028D3 File Offset: 0x00000AD3
		protected SeriesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.seriesId = (string)info.GetValue("seriesId", typeof(string));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028FD File Offset: 0x00000AFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("seriesId", this.seriesId);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002918 File Offset: 0x00000B18
		public string SeriesId
		{
			get
			{
				return this.seriesId;
			}
		}

		// Token: 0x0400002E RID: 46
		private readonly string seriesId;
	}
}
