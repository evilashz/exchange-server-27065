using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010A0 RID: 4256
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class XmlMissingElementException : LocalizedException
	{
		// Token: 0x0600B220 RID: 45600 RVA: 0x002996E9 File Offset: 0x002978E9
		public XmlMissingElementException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B221 RID: 45601 RVA: 0x002996F2 File Offset: 0x002978F2
		public XmlMissingElementException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B222 RID: 45602 RVA: 0x002996FC File Offset: 0x002978FC
		protected XmlMissingElementException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B223 RID: 45603 RVA: 0x00299706 File Offset: 0x00297906
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
