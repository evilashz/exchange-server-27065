using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FD0 RID: 4048
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TestWebServicesTaskException : TaskException
	{
		// Token: 0x0600ADE5 RID: 44517 RVA: 0x002925F7 File Offset: 0x002907F7
		public TestWebServicesTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600ADE6 RID: 44518 RVA: 0x00292600 File Offset: 0x00290800
		public TestWebServicesTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600ADE7 RID: 44519 RVA: 0x0029260A File Offset: 0x0029080A
		protected TestWebServicesTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADE8 RID: 44520 RVA: 0x00292614 File Offset: 0x00290814
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
