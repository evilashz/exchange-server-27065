using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200011D RID: 285
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OscContactSourcesForContactParseException : LocalizedException
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x0006A424 File Offset: 0x00068624
		public OscContactSourcesForContactParseException(string errMessage) : base(ServerStrings.idOscContactSourcesForContactParseError(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0006A439 File Offset: 0x00068639
		public OscContactSourcesForContactParseException(string errMessage, Exception innerException) : base(ServerStrings.idOscContactSourcesForContactParseError(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0006A44F File Offset: 0x0006864F
		protected OscContactSourcesForContactParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0006A479 File Offset: 0x00068679
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0006A494 File Offset: 0x00068694
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x040009AA RID: 2474
		private readonly string errMessage;
	}
}
