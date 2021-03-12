using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001001 RID: 4097
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PhotoMustNotBeBlankException : LocalizedException
	{
		// Token: 0x0600AEC2 RID: 44738 RVA: 0x002935F9 File Offset: 0x002917F9
		public PhotoMustNotBeBlankException() : base(Strings.PhotoMustNotBeBlank)
		{
		}

		// Token: 0x0600AEC3 RID: 44739 RVA: 0x00293606 File Offset: 0x00291806
		public PhotoMustNotBeBlankException(Exception innerException) : base(Strings.PhotoMustNotBeBlank, innerException)
		{
		}

		// Token: 0x0600AEC4 RID: 44740 RVA: 0x00293614 File Offset: 0x00291814
		protected PhotoMustNotBeBlankException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEC5 RID: 44741 RVA: 0x0029361E File Offset: 0x0029181E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
