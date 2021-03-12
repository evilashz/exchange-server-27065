using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EventLogTasks
{
	// Token: 0x02000FCA RID: 4042
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidEventCategoryInputException : LocalizedException
	{
		// Token: 0x0600ADC8 RID: 44488 RVA: 0x0029235C File Offset: 0x0029055C
		public InvalidEventCategoryInputException() : base(Strings.InvalidCategoryObject)
		{
		}

		// Token: 0x0600ADC9 RID: 44489 RVA: 0x00292369 File Offset: 0x00290569
		public InvalidEventCategoryInputException(Exception innerException) : base(Strings.InvalidCategoryObject, innerException)
		{
		}

		// Token: 0x0600ADCA RID: 44490 RVA: 0x00292377 File Offset: 0x00290577
		protected InvalidEventCategoryInputException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADCB RID: 44491 RVA: 0x00292381 File Offset: 0x00290581
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
