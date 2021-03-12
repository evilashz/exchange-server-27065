using System;
using System.Globalization;
using Microsoft.Exchange.AddressBook.Service;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.AddressBook.Nspi
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PhotoRequestAddressbookLogger : IPerformanceDataLogger
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00011813 File Offset: 0x0000FA13
		internal PhotoRequestAddressbookLogger(ProtocolLogSession logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this.logger = logger;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00011830 File Offset: 0x0000FA30
		public void Log(string marker, string counter, TimeSpan dataPoint)
		{
			if (PhotoRequestAddressbookLogger.ShouldLogMarker(marker))
			{
				ProtocolLogSession protocolLogSession;
				(protocolLogSession = this.logger)[ProtocolLog.Field.OperationSpecific] = protocolLogSession[ProtocolLog.Field.OperationSpecific] + string.Format(CultureInfo.InvariantCulture, ";{0}.{1}={2}", new object[]
				{
					marker,
					counter,
					dataPoint.TotalMilliseconds
				});
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00011890 File Offset: 0x0000FA90
		public void Log(string marker, string counter, uint dataPoint)
		{
			if (PhotoRequestAddressbookLogger.ShouldLogMarker(marker))
			{
				ProtocolLogSession protocolLogSession;
				(protocolLogSession = this.logger)[ProtocolLog.Field.OperationSpecific] = protocolLogSession[ProtocolLog.Field.OperationSpecific] + string.Format(CultureInfo.InvariantCulture, ";{0}.{1}={2}", new object[]
				{
					marker,
					counter,
					dataPoint
				});
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000118EC File Offset: 0x0000FAEC
		public void Log(string marker, string counter, string dataPoint)
		{
			if (PhotoRequestAddressbookLogger.ShouldLogMarker(marker))
			{
				ProtocolLogSession protocolLogSession;
				(protocolLogSession = this.logger)[ProtocolLog.Field.OperationSpecific] = protocolLogSession[ProtocolLog.Field.OperationSpecific] + string.Format(CultureInfo.InvariantCulture, ";{0}.{1}={2}", new object[]
				{
					marker,
					counter,
					dataPoint
				});
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011940 File Offset: 0x0000FB40
		public void AppendToLog(string logEntry)
		{
			ProtocolLogSession protocolLogSession;
			(protocolLogSession = this.logger)[ProtocolLog.Field.OperationSpecific] = protocolLogSession[ProtocolLog.Field.OperationSpecific] + string.Format(CultureInfo.InvariantCulture, ";GPP.{0}", new object[]
			{
				logEntry
			});
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011984 File Offset: 0x0000FB84
		private static bool ShouldLogMarker(string marker)
		{
			return "getuserphotototal".Equals(marker, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000187 RID: 391
		private const string GetUserPhotoLatencyMarker = "getuserphotototal";

		// Token: 0x04000188 RID: 392
		private readonly ProtocolLogSession logger;
	}
}
