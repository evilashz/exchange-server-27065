using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011AA RID: 4522
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LanguagesNotPassed : LocalizedException
	{
		// Token: 0x0600B841 RID: 47169 RVA: 0x002A45E1 File Offset: 0x002A27E1
		public LanguagesNotPassed() : base(Strings.LanguagesNotPassed)
		{
		}

		// Token: 0x0600B842 RID: 47170 RVA: 0x002A45EE File Offset: 0x002A27EE
		public LanguagesNotPassed(Exception innerException) : base(Strings.LanguagesNotPassed, innerException)
		{
		}

		// Token: 0x0600B843 RID: 47171 RVA: 0x002A45FC File Offset: 0x002A27FC
		protected LanguagesNotPassed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B844 RID: 47172 RVA: 0x002A4606 File Offset: 0x002A2806
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
