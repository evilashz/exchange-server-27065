using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007C8 RID: 1992
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RuleTableRestriction : IModifyTableRestriction
	{
		// Token: 0x06004AC7 RID: 19143 RVA: 0x0013926F File Offset: 0x0013746F
		internal RuleTableRestriction(CoreFolder coreFolder)
		{
			Util.ThrowOnNullArgument(coreFolder, "coreFolder");
			this.coreFolder = coreFolder;
			this.session = coreFolder.Session;
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00139298 File Offset: 0x00137498
		void IModifyTableRestriction.Enforce(IModifyTable modifyTable, IEnumerable<ModifyTableOperation> modifyTableOperations)
		{
			if (modifyTableOperations == null)
			{
				return;
			}
			if (this.session != null && this.session.IsPublicFolderSession && !this.session.IsMoveUser && this.HasCreateOrModifyReplyRule(modifyTableOperations) && !this.coreFolder.IsMailEnabled())
			{
				throw new RuleNotSupportedException(ServerStrings.ReplyRuleNotSupportedOnNonMailPublicFolder);
			}
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x001392EC File Offset: 0x001374EC
		private bool HasCreateOrModifyReplyRule(IEnumerable<ModifyTableOperation> modifyTableOperations)
		{
			foreach (ModifyTableOperation modifyTableOperation in modifyTableOperations)
			{
				if (modifyTableOperation.Operation == ModifyTableOperationType.Add || modifyTableOperation.Operation == ModifyTableOperationType.Modify)
				{
					foreach (PropValue propValue in modifyTableOperation.Properties)
					{
						if (propValue.Property == InternalSchema.RuleActions)
						{
							RuleAction[] array = propValue.Value as RuleAction[];
							foreach (RuleAction ruleAction in array)
							{
								if (ruleAction.ActionType == RuleActionType.Reply)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04002894 RID: 10388
		private readonly CoreFolder coreFolder;

		// Token: 0x04002895 RID: 10389
		private readonly StoreSession session;
	}
}
