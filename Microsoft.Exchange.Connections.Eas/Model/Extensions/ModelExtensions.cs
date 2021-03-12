using System;
using Microsoft.Exchange.Connections.Eas.Commands;
using Microsoft.Exchange.Connections.Eas.Model.Common.WindowsLive;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSync;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Extensions
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ModelExtensions
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00009601 File Offset: 0x00007801
		public static EasFolderType GetEasFolderType(this Add add)
		{
			return (EasFolderType)add.Type;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009609 File Offset: 0x00007809
		public static EasFolderType GetEasFolderType(this Update update)
		{
			return (EasFolderType)update.Type;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009611 File Offset: 0x00007811
		public static WlasSystemCategoryId GetSystemCategoryId(this CategoryId categoryId)
		{
			return (WlasSystemCategoryId)categoryId.Id;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009619 File Offset: 0x00007819
		public static bool HasMoreAvailable(this Collection collection)
		{
			return collection.MoreAvailable != null;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009628 File Offset: 0x00007828
		public static FlagStatus? GetFlagStatus(this Fetch fetch)
		{
			if (fetch.Properties != null && fetch.Properties.Flag != null)
			{
				int status = fetch.Properties.Flag.Status;
				return new FlagStatus?((FlagStatus)status);
			}
			return null;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000966C File Offset: 0x0000786C
		public static bool IsRead(this Fetch fetch)
		{
			if (fetch.Properties != null && fetch.Properties.Read != null)
			{
				byte value = fetch.Properties.Read.Value;
				return value == 1;
			}
			return false;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000096B0 File Offset: 0x000078B0
		public static bool IsRead(this AddCommand addCommand)
		{
			return ModelExtensions.IsRead(addCommand.ApplicationData) ?? false;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000096DB File Offset: 0x000078DB
		public static bool? IsRead(this ChangeCommand changeCommand)
		{
			return ModelExtensions.IsRead(changeCommand.ApplicationData);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000096E8 File Offset: 0x000078E8
		private static bool? IsRead(ApplicationData applicationData)
		{
			if (applicationData != null && applicationData.Read != null)
			{
				byte value = applicationData.Read.Value;
				return new bool?(value == 1);
			}
			return null;
		}
	}
}
