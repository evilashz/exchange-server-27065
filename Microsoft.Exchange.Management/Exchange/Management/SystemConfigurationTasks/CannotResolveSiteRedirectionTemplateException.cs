using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001142 RID: 4418
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveSiteRedirectionTemplateException : LocalizedException
	{
		// Token: 0x0600B52E RID: 46382 RVA: 0x0029DCA5 File Offset: 0x0029BEA5
		public CannotResolveSiteRedirectionTemplateException() : base(Strings.CannotResolveSiteRedirectionTemplateMessage)
		{
		}

		// Token: 0x0600B52F RID: 46383 RVA: 0x0029DCB2 File Offset: 0x0029BEB2
		public CannotResolveSiteRedirectionTemplateException(Exception innerException) : base(Strings.CannotResolveSiteRedirectionTemplateMessage, innerException)
		{
		}

		// Token: 0x0600B530 RID: 46384 RVA: 0x0029DCC0 File Offset: 0x0029BEC0
		protected CannotResolveSiteRedirectionTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B531 RID: 46385 RVA: 0x0029DCCA File Offset: 0x0029BECA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
