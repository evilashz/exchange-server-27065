using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000449 RID: 1097
	internal static class RetentionPolicyValidator
	{
		// Token: 0x060026BF RID: 9919 RVA: 0x000996A0 File Offset: 0x000978A0
		private static void ValidateNoOverlappingMessageClass(RetentionPolicy retentionPolicy, IEnumerable<PresentationRetentionPolicyTag> defaultTags, Task.TaskErrorLoggingDelegate writeError)
		{
			IEnumerable<IGrouping<string, string>> source = defaultTags.SelectMany((PresentationRetentionPolicyTag x) => x.MessageClass.Split(ElcMessageClass.MessageClassDelims, StringSplitOptions.RemoveEmptyEntries)).GroupBy((string x) => x, StringComparer.OrdinalIgnoreCase);
			IGrouping<string, string> grouping = source.FirstOrDefault((IGrouping<string, string> x) => x.Count<string>() > 1);
			if (grouping != null)
			{
				writeError(new RetentionPolicyTagTaskException(Strings.ErrorDefaultTagHasConflictedMessageClasses(retentionPolicy.Id.ToString(), grouping.Key)), ErrorCategory.InvalidOperation, retentionPolicy);
			}
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000997A4 File Offset: 0x000979A4
		internal static void ValicateDefaultTags(RetentionPolicy retentionPolicy, PresentationRetentionPolicyTag[] retentionTags, Task.TaskErrorLoggingDelegate writeError)
		{
			PresentationRetentionPolicyTag[] array = (from x in retentionTags
			where x.Type == ElcFolderType.All && x.RetentionAction != RetentionActionType.MoveToArchive
			select x).ToArray<PresentationRetentionPolicyTag>();
			if (array.Count((PresentationRetentionPolicyTag x) => !x.MessageClass.Equals(ElcMessageClass.AllMailboxContent, StringComparison.OrdinalIgnoreCase) && !x.MessageClass.Equals(ElcMessageClass.VoiceMail, StringComparison.OrdinalIgnoreCase)) != 0)
			{
				writeError(new RetentionPolicyTagTaskException(Strings.ErrorIncorrectDefaultTag), ErrorCategory.InvalidOperation, retentionPolicy);
			}
			if (array.Length > 1)
			{
				RetentionPolicyValidator.ValidateNoOverlappingMessageClass(retentionPolicy, array, writeError);
			}
			array = (from x in retentionTags
			where x.Type == ElcFolderType.All && x.RetentionAction == RetentionActionType.MoveToArchive
			select x).ToArray<PresentationRetentionPolicyTag>();
			if (array.Length > 0)
			{
				RetentionPolicyValidator.ValidateNoOverlappingMessageClass(retentionPolicy, array, writeError);
			}
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00099894 File Offset: 0x00097A94
		internal static void ValidateSystemFolderTags(RetentionPolicy retentionPolicy, PresentationRetentionPolicyTag[] retentionTags, Task.TaskErrorLoggingDelegate writeError)
		{
			IEnumerable<PresentationRetentionPolicyTag> source = from x in retentionTags
			where x.RetentionEnabled && x.Type != ElcFolderType.Personal && x.Type != ElcFolderType.All
			select x;
			IGrouping<ElcFolderType, PresentationRetentionPolicyTag> grouping = (from x in source
			group x by x.Type).FirstOrDefault((IGrouping<ElcFolderType, PresentationRetentionPolicyTag> y) => y.Count<PresentationRetentionPolicyTag>() > 1);
			if (grouping != null)
			{
				PresentationRetentionPolicyTag presentationRetentionPolicyTag = grouping.First<PresentationRetentionPolicyTag>();
				writeError(new RetentionPolicyTagTaskException(Strings.ErrorMultipleSystemFolderTagConfliction(retentionPolicy.Id.ToString(), presentationRetentionPolicyTag.Type.ToString())), ErrorCategory.InvalidOperation, retentionPolicy);
			}
		}
	}
}
