using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Probes
{
	// Token: 0x0200022F RID: 559
	public class IntraDagPingProbe : ProbeWorkItem
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x000683E8 File Offset: 0x000665E8
		private static byte[] PingPacket
		{
			get
			{
				byte[] array = (byte[])IntraDagPingProbe.pingPacket.Target;
				if (array == null)
				{
					lock (IntraDagPingProbe.pingPacket)
					{
						array = (byte[])IntraDagPingProbe.pingPacket.Target;
						if (array == null)
						{
							array = new byte[1459];
							new Random().NextBytes(array);
							IntraDagPingProbe.pingPacket.Target = array;
						}
					}
				}
				return array;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00068484 File Offset: 0x00066684
		protected override void DoWork(CancellationToken cancellationToken)
		{
			string endpoint = base.Definition.Endpoint;
			if (string.IsNullOrWhiteSpace(endpoint))
			{
				throw new InvalidOperationException("The probe definition 'Endpoint' property is not set.");
			}
			int num = (base.Definition.TimeoutSeconds - 2) * 1000;
			if (num <= 0)
			{
				throw new InvalidOperationException(string.Format("The probe definition 'TimeoutSeconds' value of {0} is too short.", base.Definition.TimeoutSeconds));
			}
			byte[] array = IntraDagPingProbe.PingPacket;
			base.Result.StateAttribute1 = "Ping";
			base.Result.StateAttribute11 = IntraDagPingProbe.ExtractNameFromFqdn(endpoint);
			if (DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(base.Definition.ExecutionLocation))
			{
				string info = string.Format(CultureInfo.InvariantCulture, "Source {0} is in Maintenance Mode", new object[]
				{
					base.Definition.ExecutionLocation
				});
				this.SetResultData(99005, "MaintenanceMode", info);
			}
			else if (DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(endpoint))
			{
				string info2 = string.Format(CultureInfo.InvariantCulture, "Target {0} is in Maintenance Mode", new object[]
				{
					endpoint
				});
				this.SetResultData(99006, "MaintenanceMode", info2);
			}
			else
			{
				try
				{
					using (ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim())
					{
						using (Ping ping = new Ping())
						{
							ping.PingCompleted += this.PingCallback;
							ping.SendAsync(endpoint, num, array, Tuple.Create<byte[], ManualResetEventSlim>(array, manualResetEventSlim));
							using (cancellationToken.Register(delegate()
							{
								ping.SendAsyncCancel();
							}))
							{
								manualResetEventSlim.Wait();
							}
						}
					}
				}
				catch (SocketException)
				{
					this.SetResultData(99001, "NameResolution", "Domain Name Resolution Failure");
				}
				catch (PingException ex)
				{
					this.SetResultData(99002, "PingException", ex.Message);
				}
			}
			if (base.Result.ResultType == ResultType.Failed)
			{
				throw new ApplicationException("Ping was unsuccessful.");
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000686E0 File Offset: 0x000668E0
		private static string ExtractNameFromFqdn(string fqdn)
		{
			int num = fqdn.IndexOf('.');
			if (num >= 0)
			{
				return fqdn.Substring(0, num);
			}
			return fqdn;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00068704 File Offset: 0x00066904
		private void PingCallback(object sender, PingCompletedEventArgs a)
		{
			IPAddress targetAddress = null;
			long roundTripMs = 0L;
			Tuple<byte[], ManualResetEventSlim> tuple = (Tuple<byte[], ManualResetEventSlim>)a.UserState;
			int code;
			string reason;
			string info;
			if (a.Cancelled)
			{
				code = -2;
				reason = "Cancelled";
				info = "Probe execution cancelled.";
			}
			else if (a.Error != null)
			{
				code = 99003;
				reason = "AsyncException";
				info = a.Error.Message;
			}
			else
			{
				code = (int)a.Reply.Status;
				info = (reason = a.Reply.Status.ToString());
				targetAddress = a.Reply.Address;
				roundTripMs = a.Reply.RoundtripTime;
				if (a.Reply.Status == IPStatus.Success)
				{
					byte[] item = tuple.Item1;
					byte[] buffer = a.Reply.Buffer;
					if (!item.SequenceEqual(buffer))
					{
						code = 99004;
						reason = "PacketMismatch";
						info = "Sent packet differs from reply packet.";
					}
				}
			}
			this.SetResultData(code, reason, info, targetAddress, roundTripMs);
			tuple.Item2.Set();
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000687F7 File Offset: 0x000669F7
		private void SetResultData(int code, string reason, string info)
		{
			base.Result.StateAttribute2 = reason;
			base.Result.StateAttribute13 = info;
			base.Result.StateAttribute16 = (double)code;
			if (code != 0)
			{
				base.Result.ResultType = ResultType.Failed;
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0006882D File Offset: 0x00066A2D
		private void SetResultData(int code, string reason, string info, IPAddress targetAddress, long roundTripMs)
		{
			this.SetResultData(code, reason, info);
			base.Result.StateAttribute12 = ((targetAddress == null) ? null : targetAddress.ToString());
			base.Result.StateAttribute17 = (double)roundTripMs;
			base.Result.SampleValue = (double)roundTripMs;
		}

		// Token: 0x04000BC9 RID: 3017
		private const int PacketSize = 1459;

		// Token: 0x04000BCA RID: 3018
		private static WeakReference pingPacket = new WeakReference(null);
	}
}
