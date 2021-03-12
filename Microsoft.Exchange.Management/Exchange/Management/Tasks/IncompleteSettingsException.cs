using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FB8 RID: 4024
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncompleteSettingsException : LocalizedException
	{
		// Token: 0x0600AD70 RID: 44400 RVA: 0x00291B60 File Offset: 0x0028FD60
		public IncompleteSettingsException() : base(Strings.ErrorIncompleteSettings)
		{
		}

		// Token: 0x0600AD71 RID: 44401 RVA: 0x00291B6D File Offset: 0x0028FD6D
		public IncompleteSettingsException(Exception innerException) : base(Strings.ErrorIncompleteSettings, innerException)
		{
		}

		// Token: 0x0600AD72 RID: 44402 RVA: 0x00291B7B File Offset: 0x0028FD7B
		protected IncompleteSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD73 RID: 44403 RVA: 0x00291B85 File Offset: 0x0028FD85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
