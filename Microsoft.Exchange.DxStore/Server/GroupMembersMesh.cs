using System;
using System.Collections.Concurrent;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using FUSE.Paxos;
using FUSE.Paxos.Network;
using FUSE.Weld.Base;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x0200005D RID: 93
	public sealed class GroupMembersMesh : ISubject<Tuple<string, Message>>, ISubject<Tuple<string, Message>, Tuple<string, Message>>, IObserver<Tuple<string, Message>>, IObservable<Tuple<string, Message>>
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public GroupMembersMesh(string identity, INodeEndPoints<ServiceEndpoint> nodeEndPoints, InstanceGroupConfig groupConfig)
		{
			this.groupConfig = groupConfig;
			this.identity = identity;
			this.nodeEndPoints = nodeEndPoints;
			this.incoming = Subject.Synchronize<Tuple<string, Message>, Tuple<string, Message>>(new Subject<Tuple<string, Message>>(), Scheduler.TaskPool);
			this.factoryByEndPoint = new ConcurrentDictionary<ServiceEndpoint, WCF.CachedChannelFactory<IDxStoreInstance>>();
			ObservableExtensions.Subscribe<Tuple<string, Message>>(this.incoming, delegate(Tuple<string, Message> item)
			{
				this.TraceMessage(false, item);
			});
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00009E4F File Offset: 0x0000804F
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MeshTracer;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00009E56 File Offset: 0x00008056
		public ISubject<Tuple<string, Message>, Tuple<string, Message>> Incoming
		{
			get
			{
				return this.incoming;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00009E5E File Offset: 0x0000805E
		public void OnCompleted()
		{
			GroupMembersMesh.Tracer.TraceDebug<string>((long)this.identity.GetHashCode(), "{0}: OnCompleted() called", this.identity);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00009E81 File Offset: 0x00008081
		public void OnError(Exception exception)
		{
			if (GroupMembersMesh.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				GroupMembersMesh.Tracer.TraceError<string, Exception>((long)this.identity.GetHashCode(), "{0}: OnError() called with {1}", this.identity, exception);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00009EC0 File Offset: 0x000080C0
		public ChannelFactory<IDxStoreInstance> GetChannelFactory(string nodeName)
		{
			ServiceEndpoint key = this.nodeEndPoints.Map(nodeName);
			WCF.CachedChannelFactory<IDxStoreInstance> orAdd = this.factoryByEndPoint.GetOrAdd(key, delegate(ServiceEndpoint e)
			{
				WCF.Initialize(e);
				return new WCF.CachedChannelFactory<IDxStoreInstance>(e);
			});
			return orAdd.Factory;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009F28 File Offset: 0x00008128
		public void OnNext(Tuple<string, Message> item)
		{
			Concurrency.SwallowExceptions(Task.Run(() => this.OnNextAsync(item)), null, new object[0]);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009F67 File Offset: 0x00008167
		public IDisposable Subscribe(IObserver<Tuple<string, Message>> observer)
		{
			GroupMembersMesh.Tracer.TraceDebug<string>((long)this.identity.GetHashCode(), "{0}: Subscription requested for incoming messages", this.identity);
			return this.Incoming.Subscribe(observer);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009F98 File Offset: 0x00008198
		private void TraceMessage(bool isSend, Tuple<string, Message> msg)
		{
			if (ExTraceGlobals.PaxosMessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				int num = 80;
				string text = msg.Item2.ToString();
				if (text.Length < num)
				{
					num = text.Length;
				}
				ExTraceGlobals.PaxosMessageTracer.TraceDebug((long)this.identity.GetHashCode(), "{0}: {1} {2} : {3}", new object[]
				{
					this.identity,
					isSend ? "Send ->" : "Recv <-",
					msg.Item1,
					text.Substring(0, num)
				});
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000A023 File Offset: 0x00008223
		private bool IsSelf(string target)
		{
			return Utils.IsEqual(this.groupConfig.Self, target, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A3E4 File Offset: 0x000085E4
		private async Task OnNextAsync(Tuple<string, Message> item)
		{
			try
			{
				this.TraceMessage(true, item);
				if (this.IsSelf(item.Item1))
				{
					this.Incoming.OnNext(item);
				}
				else if (!this.groupConfig.Settings.IsUseHttpTransportForInstanceCommunication)
				{
					ChannelFactory<IDxStoreInstance> factory = this.GetChannelFactory(item.Item1);
					await Concurrency.DropContext(WCF.WithServiceAsync<IDxStoreInstance>(factory, (IDxStoreInstance instance) => instance.PaxosMessageAsync(this.groupConfig.Self, item.Item2), null, default(CancellationToken)));
				}
				else
				{
					HttpRequest.PaxosMessage msg = new HttpRequest.PaxosMessage(this.nodeEndPoints.Self, item.Item2);
					string targetHost = this.groupConfig.GetMemberNetworkAddress(item.Item1);
					await HttpClient.SendMessageAsync(targetHost, item.Item1, this.groupConfig.Name, msg);
				}
			}
			catch (Exception ex)
			{
				if (GroupMembersMesh.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					GroupMembersMesh.Tracer.TraceError((long)this.identity.GetHashCode(), "{0}: OnNextAsync(Node:{1}, Msg:{2}) failed with {3}", new object[]
					{
						this.identity,
						item.Item1,
						item.Item2,
						ex
					});
				}
			}
		}

		// Token: 0x040001D7 RID: 471
		private readonly INodeEndPoints<ServiceEndpoint> nodeEndPoints;

		// Token: 0x040001D8 RID: 472
		private readonly ConcurrentDictionary<ServiceEndpoint, WCF.CachedChannelFactory<IDxStoreInstance>> factoryByEndPoint;

		// Token: 0x040001D9 RID: 473
		private readonly ISubject<Tuple<string, Message>, Tuple<string, Message>> incoming;

		// Token: 0x040001DA RID: 474
		private readonly string identity;

		// Token: 0x040001DB RID: 475
		private readonly InstanceGroupConfig groupConfig;
	}
}
