using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.LiveIDAuthentication;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AlternateWlidEndpointHandler
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0001009B File Offset: 0x0000E29B
		internal AlternateWlidEndpointHandler(string registryKeyName, SyncLogSession syncLogSession, Trace tracer)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("registryKeyName", registryKeyName);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("tracer", tracer);
			this.registryKeyName = registryKeyName;
			this.syncLogSession = syncLogSession;
			this.tracer = tracer;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000100DC File Offset: 0x0000E2DC
		internal void SetWlidEndpoint(LiveIDAuthenticationClient authenticationClient)
		{
			Uri uri = null;
			Exception ex;
			this.alternateWlidLoaded = this.TryLoadAlternateWlidEndpoint(out uri, out ex);
			if (this.alternateWlidLoaded)
			{
				this.syncLogSession.LogDebugging((TSLID)664UL, this.tracer, "A valid alternate wlid endpoint found in registry: {0}", new object[]
				{
					uri
				});
				this.originalAuthServerUri = authenticationClient.AuthServerUri;
				this.syncLogSession.LogDebugging((TSLID)665UL, this.tracer, "Backing up originalAuthServerUri: {0}", new object[]
				{
					this.originalAuthServerUri
				});
				authenticationClient.AuthServerUri = uri;
				return;
			}
			this.syncLogSession.LogError((TSLID)666UL, this.tracer, "No valid alternate wlid endpoint found, error (if any): {0}", new object[]
			{
				ex
			});
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000101A6 File Offset: 0x0000E3A6
		internal void RestoreWlidEndpoint(LiveIDAuthenticationClient authenticationClient)
		{
			if (authenticationClient != null && this.alternateWlidLoaded)
			{
				authenticationClient.AuthServerUri = this.originalAuthServerUri;
				this.alternateWlidLoaded = false;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000101C8 File Offset: 0x0000E3C8
		private bool TryLoadAlternateWlidEndpoint(out Uri alternateWlidUri, out Exception exception)
		{
			alternateWlidUri = null;
			exception = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(AlternateWlidEndpointHandler.AlternateWlidEndpointLocation))
				{
					if (registryKey != null)
					{
						string text = registryKey.GetValue(this.registryKeyName) as string;
						if (!string.IsNullOrEmpty(text))
						{
							return Uri.TryCreate(text, UriKind.Absolute, out alternateWlidUri);
						}
					}
				}
			}
			catch (SecurityException ex)
			{
				exception = ex;
			}
			catch (IOException ex2)
			{
				exception = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				exception = ex3;
			}
			return false;
		}

		// Token: 0x040001DF RID: 479
		internal static readonly string AlternateWlidEndpointLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\TransportSync\\Wlid\\";

		// Token: 0x040001E0 RID: 480
		private readonly SyncLogSession syncLogSession;

		// Token: 0x040001E1 RID: 481
		private readonly Trace tracer;

		// Token: 0x040001E2 RID: 482
		private Uri originalAuthServerUri;

		// Token: 0x040001E3 RID: 483
		private bool alternateWlidLoaded;

		// Token: 0x040001E4 RID: 484
		private string registryKeyName;
	}
}
