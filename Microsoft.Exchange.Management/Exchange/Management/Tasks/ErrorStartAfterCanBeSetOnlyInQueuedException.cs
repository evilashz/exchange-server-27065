using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E9B RID: 3739
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorStartAfterCanBeSetOnlyInQueuedException : RecipientTaskException
	{
		// Token: 0x0600A7E2 RID: 42978 RVA: 0x00289319 File Offset: 0x00287519
		public ErrorStartAfterCanBeSetOnlyInQueuedException() : base(Strings.ErrorStartAfterCanBeSetOnlyInQueued)
		{
		}

		// Token: 0x0600A7E3 RID: 42979 RVA: 0x00289326 File Offset: 0x00287526
		public ErrorStartAfterCanBeSetOnlyInQueuedException(Exception innerException) : base(Strings.ErrorStartAfterCanBeSetOnlyInQueued, innerException)
		{
		}

		// Token: 0x0600A7E4 RID: 42980 RVA: 0x00289334 File Offset: 0x00287534
		protected ErrorStartAfterCanBeSetOnlyInQueuedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7E5 RID: 42981 RVA: 0x0028933E File Offset: 0x0028753E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
