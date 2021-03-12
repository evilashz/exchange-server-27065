using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E3F RID: 3647
	internal static class CustomActions
	{
		// Token: 0x06005DFB RID: 24059 RVA: 0x00124814 File Offset: 0x00122A14
		internal static void RegisterAction(EdmModel model, EdmEntityType entityType, EdmEntityType returnType, string actionName, IDictionary<string, IEdmTypeReference> parameters)
		{
			ArgumentValidator.ThrowIfNull("model", model);
			ArgumentValidator.ThrowIfNull("entityType", entityType);
			ArgumentValidator.ThrowIfNullOrEmpty("actionName", actionName);
			EdmAction edmAction = new EdmAction(entityType.Namespace, actionName, (returnType == null) ? null : new EdmEntityTypeReference(returnType, true), true, new EdmPathExpression("bindingParameter"));
			EdmOperationParameter edmOperationParameter = new EdmOperationParameter(edmAction, "bindingParameter", new EdmEntityTypeReference(entityType, true));
			edmAction.AddParameter(edmOperationParameter);
			if (parameters != null)
			{
				foreach (KeyValuePair<string, IEdmTypeReference> keyValuePair in parameters)
				{
					EdmOperationParameter edmOperationParameter2 = new EdmOperationParameter(edmAction, keyValuePair.Key, keyValuePair.Value);
					edmAction.AddParameter(edmOperationParameter2);
				}
			}
			model.AddElement(edmAction);
		}

		// Token: 0x040032AB RID: 12971
		public const string Copy = "Copy";

		// Token: 0x040032AC RID: 12972
		public const string Move = "Move";

		// Token: 0x040032AD RID: 12973
		public const string TentativelyAccept = "TentativelyAccept";

		// Token: 0x040032AE RID: 12974
		public const string Accept = "Accept";

		// Token: 0x040032AF RID: 12975
		public const string Decline = "Decline";

		// Token: 0x040032B0 RID: 12976
		public const string CreateReply = "CreateReply";

		// Token: 0x040032B1 RID: 12977
		public const string CreateReplyAll = "CreateReplyAll";

		// Token: 0x040032B2 RID: 12978
		public const string CreateForward = "CreateForward";

		// Token: 0x040032B3 RID: 12979
		public const string Reply = "Reply";

		// Token: 0x040032B4 RID: 12980
		public const string ReplyAll = "ReplyAll";

		// Token: 0x040032B5 RID: 12981
		public const string Forward = "Forward";

		// Token: 0x040032B6 RID: 12982
		public const string Send = "Send";

		// Token: 0x02000E40 RID: 3648
		internal static class Parameters
		{
			// Token: 0x040032B7 RID: 12983
			public const string DestinationId = "DestinationId";

			// Token: 0x040032B8 RID: 12984
			public const string Comment = "Comment";

			// Token: 0x040032B9 RID: 12985
			public const string ToRecipients = "ToRecipients";
		}
	}
}
