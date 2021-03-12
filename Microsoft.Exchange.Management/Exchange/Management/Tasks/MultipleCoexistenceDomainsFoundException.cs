using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200119C RID: 4508
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultipleCoexistenceDomainsFoundException : LocalizedException
	{
		// Token: 0x0600B700 RID: 46848 RVA: 0x002A0CA9 File Offset: 0x0029EEA9
		public MultipleCoexistenceDomainsFoundException() : base(Strings.MultipleCoexistenceDomainsFoundException)
		{
		}

		// Token: 0x0600B701 RID: 46849 RVA: 0x002A0CB6 File Offset: 0x0029EEB6
		public MultipleCoexistenceDomainsFoundException(Exception innerException) : base(Strings.MultipleCoexistenceDomainsFoundException, innerException)
		{
		}

		// Token: 0x0600B702 RID: 46850 RVA: 0x002A0CC4 File Offset: 0x0029EEC4
		protected MultipleCoexistenceDomainsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B703 RID: 46851 RVA: 0x002A0CCE File Offset: 0x0029EECE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
