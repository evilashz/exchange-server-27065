using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF4 RID: 4084
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorMissingNameOrDescriptionOrIsDefaultParametersException : LocalizedException
	{
		// Token: 0x0600AE88 RID: 44680 RVA: 0x002931C5 File Offset: 0x002913C5
		public ErrorMissingNameOrDescriptionOrIsDefaultParametersException() : base(Strings.ErrorMissingNameOrDescriptionOrIsDefaultParameters)
		{
		}

		// Token: 0x0600AE89 RID: 44681 RVA: 0x002931D2 File Offset: 0x002913D2
		public ErrorMissingNameOrDescriptionOrIsDefaultParametersException(Exception innerException) : base(Strings.ErrorMissingNameOrDescriptionOrIsDefaultParameters, innerException)
		{
		}

		// Token: 0x0600AE8A RID: 44682 RVA: 0x002931E0 File Offset: 0x002913E0
		protected ErrorMissingNameOrDescriptionOrIsDefaultParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE8B RID: 44683 RVA: 0x002931EA File Offset: 0x002913EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
