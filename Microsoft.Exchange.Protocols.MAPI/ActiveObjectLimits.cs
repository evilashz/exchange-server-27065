using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200006F RID: 111
	public sealed class ActiveObjectLimits
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0001BA6C File Offset: 0x00019C6C
		public static long EffectiveLimitation(ClientType clientType)
		{
			switch (clientType)
			{
			case ClientType.System:
			case ClientType.Administrator:
				return ConfigurationSchema.PerAdminSessionLimit.Value;
			case ClientType.User:
				break;
			default:
				if (clientType != ClientType.MoMT)
				{
					return ConfigurationSchema.PerOtherSessionLimit.Value;
				}
				break;
			}
			return ConfigurationSchema.PerUserSessionLimit.Value;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		public static long EffectiveLimitation(MapiObjectTrackedType trackedType)
		{
			ConfigurationSchema<long> configurationSchema = null;
			if (ActiveObjectLimits.perSessionLimitationMap.TryGetValue(trackedType, out configurationSchema) && configurationSchema != null)
			{
				return configurationSchema.Value;
			}
			return -1L;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001BADE File Offset: 0x00019CDE
		public static long EffectiveLimitation(MapiServiceType serviceType)
		{
			return ConfigurationSchema.PerServiceSessionLimit.Value;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001BAEC File Offset: 0x00019CEC
		internal static void Initialize()
		{
			ActiveObjectLimits.perSessionLimitationMap = new Dictionary<MapiObjectTrackedType, ConfigurationSchema<long>>();
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.Folder, ConfigurationSchema.PerSessionFolderLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.Message, ConfigurationSchema.PerSessionMessageLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.Attachment, ConfigurationSchema.PerSessionAttachmentLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.Stream, ConfigurationSchema.PerSessionStreamLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.Notify, ConfigurationSchema.PerSessionNotifyLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.FolderView, ConfigurationSchema.PerSessionFolderViewLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.MessageView, ConfigurationSchema.PerSessionMessageViewLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.AttachmentView, ConfigurationSchema.PerSessionAttachmentViewLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.PermissionView, ConfigurationSchema.PerSessionACLViewLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.FastTransferSource, ConfigurationSchema.PerSessionFxSrcLimit);
			ActiveObjectLimits.perSessionLimitationMap.Add(MapiObjectTrackedType.FastTransferDestination, ConfigurationSchema.PerSessionFxDstLimit);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001BBB7 File Offset: 0x00019DB7
		internal static void Terminate()
		{
		}

		// Token: 0x0400022D RID: 557
		private static Dictionary<MapiObjectTrackedType, ConfigurationSchema<long>> perSessionLimitationMap;
	}
}
