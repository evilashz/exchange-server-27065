using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ConnectSubscriptionTaskKnownExceptions
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000063BC File Offset: 0x000045BC
		internal static bool IsKnown(Exception exception)
		{
			return typeof(StoragePermanentException).IsInstanceOfType(exception) || typeof(StorageTransientException).IsInstanceOfType(exception) || typeof(FacebookAuthenticationException).IsInstanceOfType(exception) || typeof(LinkedInAuthenticationException).IsInstanceOfType(exception) || typeof(ExchangeConfigurationException).IsInstanceOfType(exception) || typeof(CannotSwitchLinkedInAccountException).IsInstanceOfType(exception) || typeof(FailedDeletePeopleConnectSubscriptionException).IsInstanceOfType(exception) || typeof(ProtocolViolationException).IsInstanceOfType(exception) || typeof(WebException).IsInstanceOfType(exception) || typeof(TimeoutException).IsInstanceOfType(exception) || typeof(CryptographicException).IsInstanceOfType(exception) || typeof(InvalidDataException).IsInstanceOfType(exception) || ConnectSubscription.IsDkmException(exception);
		}
	}
}
