using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class InterbrokerMessage
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009EAE File Offset: 0x000080AE
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009EB6 File Offset: 0x000080B6
		[DataMember(EmitDefaultValue = false)]
		public Version Version { get; set; }

		// Token: 0x060001AD RID: 429
		public abstract string ToJson();
	}
}
