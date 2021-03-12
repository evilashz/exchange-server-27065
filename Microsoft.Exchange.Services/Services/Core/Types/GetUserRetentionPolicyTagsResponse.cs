using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000525 RID: 1317
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetUserRetentionPolicyTagsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserRetentionPolicyTagsResponse : ResponseMessage
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x000A5EBA File Offset: 0x000A40BA
		public GetUserRetentionPolicyTagsResponse()
		{
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000A5ECD File Offset: 0x000A40CD
		internal GetUserRetentionPolicyTagsResponse(ServiceResultCode code, ServiceError error, RetentionPolicyTag[] retentionPolicyTags) : base(code, error)
		{
			if (retentionPolicyTags != null && retentionPolicyTags.Length > 0)
			{
				this.retentionPolicyTags.AddRange(retentionPolicyTags);
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000A5EF7 File Offset: 0x000A40F7
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x000A5F04 File Offset: 0x000A4104
		[XmlArrayItem("RetentionPolicyTag", Type = typeof(RetentionPolicyTag), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray]
		[DataMember(Name = "RetentionPolicyTags", IsRequired = false)]
		public RetentionPolicyTag[] RetentionPolicyTags
		{
			get
			{
				return this.retentionPolicyTags.ToArray();
			}
			set
			{
				this.retentionPolicyTags.Clear();
				if (value != null && value.Length > 0)
				{
					this.retentionPolicyTags.AddRange(value);
				}
			}
		}

		// Token: 0x040015D7 RID: 5591
		private List<RetentionPolicyTag> retentionPolicyTags = new List<RetentionPolicyTag>();
	}
}
