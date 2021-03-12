using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.Settings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Settings
{
	// Token: 0x02000069 RID: 105
	[XmlRoot(Namespace = "Settings", ElementName = "Settings")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Settings", TypeName = "SettingsResponse")]
	public class SettingsResponse : Settings, IEasServerResponse<SettingsStatus>, IHaveAnHttpStatus
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000055C6 File Offset: 0x000037C6
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000055CE File Offset: 0x000037CE
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x060001DB RID: 475 RVA: 0x000055D7 File Offset: 0x000037D7
		bool IEasServerResponse<SettingsStatus>.IsSucceeded(SettingsStatus status)
		{
			return SettingsStatus.Success == status;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000055E0 File Offset: 0x000037E0
		SettingsStatus IEasServerResponse<SettingsStatus>.ConvertStatusToEnum()
		{
			byte status = base.Status;
			if (!SettingsResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (SettingsStatus)status;
			}
			return SettingsResponse.StatusToEnumMap[status];
		}

		// Token: 0x040001A0 RID: 416
		private static readonly IReadOnlyDictionary<byte, SettingsStatus> StatusToEnumMap = new Dictionary<byte, SettingsStatus>
		{
			{
				1,
				SettingsStatus.Success
			},
			{
				2,
				SettingsStatus.ProtocolError
			},
			{
				3,
				SettingsStatus.AccessDenied
			},
			{
				4,
				SettingsStatus.ServerUnavailable
			},
			{
				5,
				SettingsStatus.InvalidArguments
			},
			{
				6,
				SettingsStatus.ConflictingArguments
			},
			{
				7,
				SettingsStatus.DeniedByPolicy
			}
		};
	}
}
