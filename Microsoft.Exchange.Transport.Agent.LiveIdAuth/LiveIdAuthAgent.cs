using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Authentication;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Transport.Agent.LiveIdAuth
{
	// Token: 0x02000002 RID: 2
	internal sealed class LiveIdAuthAgent : SmtpReceiveAgent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public LiveIdAuthAgent()
		{
			this.liveAuth = new LiveIdBasicAuthentication();
			this.liveAuth.ApplicationName = "Microsoft.Exchange.SMTP";
			base.OnProcessAuthentication += this.BeginLiveIdAuth;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002108 File Offset: 0x00000308
		private static byte[] GetPasswordFromSecureString(SecureString password)
		{
			IntPtr intPtr = IntPtr.Zero;
			char[] array = new char[password.Length];
			byte[] array2;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(password);
				Marshal.Copy(intPtr, array, 0, password.Length);
				int num = password.Length;
				int num2 = password.Length - 1;
				while (num2 >= 1 && array[num2] == '\0' && array[num2 - 1] == '\0')
				{
					num--;
					num2--;
				}
				array2 = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = (byte)array[i];
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				Array.Clear(array, 0, array.Length);
			}
			return array2;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021C0 File Offset: 0x000003C0
		private void BeginLiveIdAuth(ReceiveCommandEventSource source, ProcessAuthenticationEventArgs args)
		{
			this.args = args;
			this.asyncContext = base.GetAgentAsyncContext();
			if (this.args.SmtpSession.LastExternalIPAddress != null)
			{
				this.liveAuth.UserIpAddress = this.args.SmtpSession.LastExternalIPAddress.ToString();
			}
			else
			{
				this.liveAuth.UserIpAddress = null;
			}
			this.liveAuth.BeginGetWindowsIdentity(this.args.UserName, LiveIdAuthAgent.GetPasswordFromSecureString(this.args.Password), new AsyncCallback(this.OnLiveIdAuthCallback), null, default(Guid));
			ExTraceGlobals.DefaultTracer.TraceDebug<string>((long)this.GetHashCode(), "Attempt to authenticate Live account {0}", Encoding.ASCII.GetString(this.args.UserName));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002288 File Offset: 0x00000488
		private void OnLiveIdAuthCallback(IAsyncResult ar)
		{
			this.Resume();
			WindowsIdentity windowsIdentity;
			this.args.AuthResult = this.liveAuth.EndGetWindowsIdentity(ar, out windowsIdentity);
			this.args.Identity = windowsIdentity;
			this.args.AuthErrorDetails = this.liveAuth.LastRequestErrorMessage;
			string text = null;
			try
			{
				text = ((windowsIdentity != null) ? windowsIdentity.Name : string.Empty);
			}
			catch (IdentityNotMappedException)
			{
				this.args.Identity = null;
			}
			catch (SystemException)
			{
				this.args.Identity = null;
			}
			ExTraceGlobals.DefaultTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Live account {0} authenticated as {1}", Encoding.ASCII.GetString(this.args.UserName), (!string.IsNullOrEmpty(text)) ? text : "Anonymous");
			this.AsyncCompleted();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000236C File Offset: 0x0000056C
		private void AsyncCompleted()
		{
			if (this.asyncContext != null)
			{
				AgentAsyncContext agentAsyncContext = this.asyncContext;
				this.asyncContext = null;
				agentAsyncContext.Complete();
				return;
			}
			ExTraceGlobals.DefaultTracer.TraceError((long)this.GetHashCode(), "AsyncCompleted() was called but MEx context is null. This should never happen.");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023AC File Offset: 0x000005AC
		private void Resume()
		{
			if (this.asyncContext != null)
			{
				this.asyncContext.Resume();
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly LiveIdBasicAuthentication liveAuth;

		// Token: 0x04000002 RID: 2
		private AgentAsyncContext asyncContext;

		// Token: 0x04000003 RID: 3
		private ProcessAuthenticationEventArgs args;
	}
}
