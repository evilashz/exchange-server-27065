using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EventLogTasks
{
	// Token: 0x02000FCB RID: 4043
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NonUniqueEventCategoryInputException : LocalizedException
	{
		// Token: 0x0600ADCC RID: 44492 RVA: 0x0029238B File Offset: 0x0029058B
		public NonUniqueEventCategoryInputException() : base(Strings.NonUniqueCategoryObject)
		{
		}

		// Token: 0x0600ADCD RID: 44493 RVA: 0x00292398 File Offset: 0x00290598
		public NonUniqueEventCategoryInputException(Exception innerException) : base(Strings.NonUniqueCategoryObject, innerException)
		{
		}

		// Token: 0x0600ADCE RID: 44494 RVA: 0x002923A6 File Offset: 0x002905A6
		protected NonUniqueEventCategoryInputException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADCF RID: 44495 RVA: 0x002923B0 File Offset: 0x002905B0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
