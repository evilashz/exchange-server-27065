using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Components.ProtocolAnalysis;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Configuration;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	internal class ProtocolAnalysisData
	{
		// Token: 0x06000141 RID: 321 RVA: 0x0000A7D9 File Offset: 0x000089D9
		public ProtocolAnalysisData(DateTime currentTime, int initWindowLength)
		{
			this.algoState = ProtocolAnalysisData.SrlState.Normal;
			this.winLen = initWindowLength;
			this.SenderDBData = new DBSenderData(currentTime);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A7FC File Offset: 0x000089FC
		public void Update(DateTime currentTime, int minMsgsPerWindow, int timeSliceInterval)
		{
			if (this.SenderDBData.NumMsgs > minMsgsPerWindow && currentTime.Subtract(this.SenderDBData.StartTime).TotalHours > (double)timeSliceInterval)
			{
				this.Reset(currentTime);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A83C File Offset: 0x00008A3C
		public int AlgorithmState(int numMsgs, int blockThreshold, ProtocolAnalysisSrlSettings srlParam, string reverseDns, IPAddress senderIP)
		{
			if (numMsgs <= 0)
			{
				throw new ArgumentOutOfRangeException("numMsgs", numMsgs, "There is no new messages from the sender " + senderIP);
			}
			this.lastBlock += numMsgs;
			int num = 0;
			switch (this.algoState)
			{
			case ProtocolAnalysisData.SrlState.Normal:
				goto IL_BF;
			case ProtocolAnalysisData.SrlState.Window:
				break;
			case ProtocolAnalysisData.SrlState.Blocked:
				this.lastBlock = numMsgs;
				this.algoState = ProtocolAnalysisData.SrlState.Window;
				break;
			default:
				return num;
			}
			if (this.lastBlock < this.winLen)
			{
				ExTraceGlobals.CalculateSrlTracer.TraceDebug<IPAddress, int, int>((long)this.GetHashCode(), "Don't calculate SRL for {0} because there are only {1} messages, winlen:{2}.", senderIP, this.lastBlock, this.winLen);
				return num;
			}
			this.lastBlock -= this.winLen;
			this.algoState = ProtocolAnalysisData.SrlState.Normal;
			this.ShrinkWindow(srlParam.WinlenShrinkFactor, srlParam.MinWinLen);
			IL_BF:
			if (this.lastBlock >= this.winLen)
			{
				num = this.CalculateSrl(srlParam, reverseDns, senderIP);
				if (num < blockThreshold)
				{
					this.lastBlock = 0;
					this.ExpandWindow(srlParam.WinlenExpandFactor, srlParam.MaxWinLen);
				}
				else
				{
					this.algoState = ProtocolAnalysisData.SrlState.Blocked;
				}
			}
			return num;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A949 File Offset: 0x00008B49
		public void BlockSender(DateTime currentTime)
		{
			this.lastBlock = 0;
			this.algoState = ProtocolAnalysisData.SrlState.Blocked;
			this.SenderDBData.SenderBlocked = true;
			this.Reset(currentTime);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A96C File Offset: 0x00008B6C
		private static void ZombieAnalysis(string[] zombieKeywords, string reverseDns, IPAddress senderIP, ref int zomb_ndom, ref int zomb_nip, ref int zomb_nseg)
		{
			if (string.IsNullOrEmpty(reverseDns))
			{
				throw new ArgumentNullException("reverseDns", "The reverseDns name cannot be null or empty");
			}
			string[] array = reverseDns.Split(new char[]
			{
				'.'
			});
			zomb_ndom = array.Length;
			if (senderIP.Equals(IPAddress.Any) || senderIP.Equals(IPAddress.Broadcast) || senderIP.Equals(IPAddress.IPv6Any) || senderIP.Equals(IPAddress.IPv6Loopback) || senderIP.Equals(IPAddress.IPv6None) || senderIP.Equals(IPAddress.Loopback) || senderIP.Equals(IPAddress.None))
			{
				zomb_nip = 0;
			}
			else
			{
				AddressFamily addressFamily = senderIP.AddressFamily;
				if (addressFamily != AddressFamily.InterNetwork)
				{
					if (addressFamily == AddressFamily.InterNetworkV6)
					{
						string[] array2 = senderIP.ToString().Split(new char[]
						{
							':'
						});
						for (int i = 0; i < array2.Length; i++)
						{
							string pattern = "(^|[^0-9a-f])" + array2[i] + "([^0-9a-f]|$)";
							if (Regex.IsMatch(reverseDns, pattern))
							{
								zomb_nip++;
							}
						}
					}
				}
				else
				{
					string[] array2 = senderIP.ToString().Split(new char[]
					{
						'.'
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string pattern2 = "(^|[^0-9])" + array2[j] + "([^0-9]|$)";
						if (Regex.IsMatch(reverseDns, pattern2))
						{
							zomb_nip++;
						}
					}
				}
			}
			zomb_nseg = 0;
			foreach (string value in zombieKeywords)
			{
				if (reverseDns.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					zomb_nseg++;
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AB00 File Offset: 0x00008D00
		private void Reset(DateTime currTime)
		{
			this.historyDBData = this.SenderDBData;
			this.SenderDBData = new DBSenderData(currTime);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AB1C File Offset: 0x00008D1C
		private void ShrinkWindow(double shrink_factor, int min_win_len)
		{
			int val = (int)Math.Round((double)this.winLen * shrink_factor);
			this.winLen = Math.Max(val, min_win_len);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000AB48 File Offset: 0x00008D48
		private void ExpandWindow(double expand_factor, int max_win_len)
		{
			int val = (int)Math.Round((double)this.winLen * expand_factor);
			this.winLen = Math.Min(val, max_win_len);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000AB74 File Offset: 0x00008D74
		private int CalculateSrl(ProtocolAnalysisSrlSettings srlParam, string reverseDns, IPAddress senderIP)
		{
			if (srlParam == null)
			{
				throw new ArgumentNullException("srlParam");
			}
			if (ProtocolAnalysisAgentFactory.SrlCalculationDisabled)
			{
				return 0;
			}
			double num = 0.0;
			if (!string.IsNullOrEmpty(reverseDns))
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				ProtocolAnalysisData.ZombieAnalysis(srlParam.ZombieKeywords, reverseDns, senderIP, ref num2, ref num3, ref num4);
				ExTraceGlobals.CalculateSrlTracer.TraceDebug((long)this.GetHashCode(), "Sender IP:{0}, ZomNDom:{1}, ZombNip:{2}, ZombNsegs:{3}", new object[]
				{
					senderIP,
					num2,
					num3,
					num4
				});
				num += (double)num2 * srlParam.ZombNdom;
				num += (double)num3 * srlParam.ZombNip;
				num += (double)num4 * srlParam.ZombNsegs;
			}
			else
			{
				num += srlParam.NullRdns;
			}
			DBSenderData combinedSenderData = this.GetCombinedSenderData();
			double num5 = Math.Log((double)(1 + combinedSenderData.NumMsgs));
			double num6 = (double)combinedSenderData.NumMsgs;
			num += num5 * srlParam.LogNumMsgs;
			ExTraceGlobals.CalculateSrlTracer.TraceDebug((long)this.GetHashCode(), "Sender IP:{0}, Msgs:{1}, ValidRepts:{2}, InvalidRcpts:{3}", new object[]
			{
				senderIP,
				combinedSenderData.NumMsgs,
				combinedSenderData.Rcpts[0],
				combinedSenderData.Rcpts[1]
			});
			num += Math.Log(1.0 + (double)combinedSenderData.Rcpts[0] / num6) * srlParam.LogNormNmsgNumValid;
			num += Math.Log(1.0 + (double)combinedSenderData.Rcpts[1] / num6) * srlParam.LogNormNmsgNumInvalid;
			ExTraceGlobals.CalculateSrlTracer.TraceDebug((long)this.GetHashCode(), "(Uniq)IP:{0}, ValidRcpt:{1}, InvalidRcpt:{2}, Helo:{3}", new object[]
			{
				senderIP,
				combinedSenderData.UniqueValidRcptCount,
				combinedSenderData.UniqueInvalidRcptCount,
				combinedSenderData.UniqueHelloDomainCount
			});
			num += Math.Log(1.0 + (double)combinedSenderData.UniqueValidRcptCount) / num6 * srlParam.LogNormNmsgNumUniqValid;
			num += Math.Log(1.0 + (double)combinedSenderData.UniqueInvalidRcptCount) / num6 * srlParam.LogNormNmsgNumUniqInvalid;
			num += Math.Log(1.0 + (double)combinedSenderData.UniqueHelloDomainCount) / num6 * srlParam.LogNormNmsgNumUniqHelo;
			ExTraceGlobals.CalculateSrlTracer.TraceDebug((long)this.GetHashCode(), "(HELO)IP:{0}, Empty:{1}, MatchAll:{2}, Match2nd:{3}, MatchLocal:{4}, NoMatch:{5}", new object[]
			{
				senderIP,
				combinedSenderData.Helo[1],
				combinedSenderData.Helo[2],
				combinedSenderData.Helo[3],
				combinedSenderData.Helo[4],
				combinedSenderData.Helo[5]
			});
			double num7 = (double)(combinedSenderData.Helo[1] + combinedSenderData.Helo[2] + combinedSenderData.Helo[3] + combinedSenderData.Helo[4] + combinedSenderData.Helo[5] + combinedSenderData.Helo[0] + 1);
			num += Math.Log(1.0 + (double)combinedSenderData.Helo[1] / num7) * srlParam.LogHeloEmpty;
			num += Math.Log(1.0 + (double)combinedSenderData.Helo[2] / num7) * srlParam.LogHeloMatchAll;
			num += Math.Log(1.0 + (double)combinedSenderData.Helo[3] / num7) * srlParam.LogHeloMatch2nd;
			num += Math.Log(1.0 + (double)combinedSenderData.Helo[4] / num7) * srlParam.LogHeloMatchLocal;
			num += Math.Log(1.0 + (double)combinedSenderData.Helo[5] / num7) * srlParam.HeloNoMatch;
			double num8 = 1.0;
			double num9 = 1.0;
			double num10 = 1.0;
			for (int i = 0; i < combinedSenderData.ValidScl.Length; i++)
			{
				num8 += (double)combinedSenderData.ValidScl[i];
			}
			for (int j = 0; j < combinedSenderData.InvalidScl.Length; j++)
			{
				num9 += (double)combinedSenderData.InvalidScl[j];
			}
			for (int k = 0; k < combinedSenderData.Length.Length; k++)
			{
				num10 += (double)combinedSenderData.Length[k];
			}
			for (int l = 0; l < combinedSenderData.ValidScl.Length; l++)
			{
				num += Math.Log(1.0 + (double)combinedSenderData.ValidScl[l] / num8) * srlParam.LogValidScl(l);
			}
			for (int m = 0; m < combinedSenderData.InvalidScl.Length; m++)
			{
				num += Math.Log(1.0 + (double)combinedSenderData.InvalidScl[m] / num9) * srlParam.LogInvalidScl(m);
			}
			for (int n = 0; n < combinedSenderData.Length.Length; n++)
			{
				num += Math.Log(1.0 + (double)combinedSenderData.Length[n] / num10) * srlParam.LogLength(n);
			}
			double num11 = (double)(combinedSenderData.Callid[0] + combinedSenderData.Callid[1] + combinedSenderData.Callid[2] + combinedSenderData.Callid[3] + combinedSenderData.Callid[4] + combinedSenderData.Callid[5] + 1);
			num += Math.Log(1.0 + (double)combinedSenderData.Callid[0] / num11) * srlParam.LogCallIdValid;
			num += Math.Log(1.0 + (double)combinedSenderData.Callid[1] / num11) * srlParam.LogCallIdInvalid;
			num += Math.Log(1.0 + (double)combinedSenderData.Callid[2] / num11) * srlParam.LogCallIdIndeterminate;
			num += Math.Log(1.0 + (double)combinedSenderData.Callid[3] / num11) * srlParam.LogCallIdEpderror;
			num += Math.Log(1.0 + (double)combinedSenderData.Callid[4] / num11) * srlParam.LogCallIdError;
			num += Math.Log(1.0 + (double)combinedSenderData.Callid[5] / num11) * srlParam.LogCallIdNull;
			num += srlParam.Bias;
			int num12 = 0;
			while (num12 < srlParam.MaxFeatureThreshold && num >= srlParam.FeatureThresholds(num12))
			{
				num12++;
			}
			return num12;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000B1F8 File Offset: 0x000093F8
		private DBSenderData GetCombinedSenderData()
		{
			DBSenderData dbsenderData = new DBSenderData(this.SenderDBData.StartTime);
			dbsenderData.Merge(this.SenderDBData);
			if (this.historyDBData != null && !this.historyDBData.SenderBlocked)
			{
				dbsenderData.Merge(this.historyDBData);
			}
			return dbsenderData;
		}

		// Token: 0x04000147 RID: 327
		public DBSenderData SenderDBData;

		// Token: 0x04000148 RID: 328
		private ProtocolAnalysisData.SrlState algoState;

		// Token: 0x04000149 RID: 329
		private int winLen;

		// Token: 0x0400014A RID: 330
		private int lastBlock;

		// Token: 0x0400014B RID: 331
		private DBSenderData historyDBData;

		// Token: 0x02000038 RID: 56
		internal enum SrlState
		{
			// Token: 0x0400014D RID: 333
			Normal,
			// Token: 0x0400014E RID: 334
			Window,
			// Token: 0x0400014F RID: 335
			Blocked
		}
	}
}
