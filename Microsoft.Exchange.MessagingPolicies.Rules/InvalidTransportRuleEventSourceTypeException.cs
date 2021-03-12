using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000AF RID: 175
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTransportRuleEventSourceTypeException : ExchangeConfigurationException
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x000181FF File Offset: 0x000163FF
		public InvalidTransportRuleEventSourceTypeException(string typeName) : base(TransportRulesStrings.InvalidTransportRuleEventSourceType(typeName))
		{
			this.typeName = typeName;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00018214 File Offset: 0x00016414
		public InvalidTransportRuleEventSourceTypeException(string typeName, Exception innerException) : base(TransportRulesStrings.InvalidTransportRuleEventSourceType(typeName), innerException)
		{
			this.typeName = typeName;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001822A File Offset: 0x0001642A
		protected InvalidTransportRuleEventSourceTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.typeName = (string)info.GetValue("typeName", typeof(string));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00018254 File Offset: 0x00016454
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("typeName", this.typeName);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001826F File Offset: 0x0001646F
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x040002E3 RID: 739
		private readonly string typeName;
	}
}
