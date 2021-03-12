using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.EventLog;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000255 RID: 597
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NonUniqueRecipientException : DataValidationException
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00078EEA File Offset: 0x000770EA
		public object AmbiguousData
		{
			get
			{
				return this.ambiguousData;
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00078EF4 File Offset: 0x000770F4
		public NonUniqueRecipientException(object ambiguousData, ValidationError error) : base(error)
		{
			this.ambiguousData = ambiguousData;
			string text = ambiguousData.ToString();
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NON_UNIQUE_RECIPIENT, text, new object[]
			{
				text
			});
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00078F2E File Offset: 0x0007712E
		protected NonUniqueRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00078F38 File Offset: 0x00077138
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x04000DD5 RID: 3541
		private object ambiguousData;
	}
}
