using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000487 RID: 1159
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Partitioner
	{
		// Token: 0x0600387E RID: 14462 RVA: 0x000D7ECE File Offset: 0x000D60CE
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>(list);
			}
			return new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000D7EEE File Offset: 0x000D60EE
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>(array);
			}
			return new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000D7F0E File Offset: 0x000D610E
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
		{
			return Partitioner.Create<TSource>(source, EnumerablePartitionerOptions.None);
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000D7F17 File Offset: 0x000D6117
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((partitionerOptions & ~EnumerablePartitionerOptions.NoBuffering) != EnumerablePartitionerOptions.None)
			{
				throw new ArgumentOutOfRangeException("partitionerOptions");
			}
			return new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, partitionerOptions);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000D7F40 File Offset: 0x000D6140
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			long num2 = (toExclusive - fromInclusive) / (long)(PlatformHelper.ProcessorCount * num);
			if (num2 == 0L)
			{
				num2 = 1L;
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000D7F7F File Offset: 0x000D617F
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0L)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000D7FAE File Offset: 0x000D61AE
		private static IEnumerable<Tuple<long, long>> CreateRanges(long fromInclusive, long toExclusive, long rangeSize)
		{
			bool shouldQuit = false;
			long i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				long item = i;
				long num;
				try
				{
					num = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num = toExclusive;
					shouldQuit = true;
				}
				if (num > toExclusive)
				{
					num = toExclusive;
				}
				yield return new Tuple<long, long>(item, num);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000D7FCC File Offset: 0x000D61CC
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			int num2 = (toExclusive - fromInclusive) / (PlatformHelper.ProcessorCount * num);
			if (num2 == 0)
			{
				num2 = 1;
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000D8009 File Offset: 0x000D6209
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000D8037 File Offset: 0x000D6237
		private static IEnumerable<Tuple<int, int>> CreateRanges(int fromInclusive, int toExclusive, int rangeSize)
		{
			bool shouldQuit = false;
			int i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				int item = i;
				int num;
				try
				{
					num = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num = toExclusive;
					shouldQuit = true;
				}
				if (num > toExclusive)
				{
					num = toExclusive;
				}
				yield return new Tuple<int, int>(item, num);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000D8058 File Offset: 0x000D6258
		private static int GetDefaultChunkSize<TSource>()
		{
			int result;
			if (typeof(TSource).IsValueType)
			{
				if (typeof(TSource).StructLayoutAttribute.Value == LayoutKind.Explicit)
				{
					result = Math.Max(1, 512 / Marshal.SizeOf(typeof(TSource)));
				}
				else
				{
					result = 128;
				}
			}
			else
			{
				result = 512 / IntPtr.Size;
			}
			return result;
		}

		// Token: 0x04001877 RID: 6263
		private const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x02000B95 RID: 2965
		private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06006D70 RID: 28016 RVA: 0x00178E87 File Offset: 0x00177087
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex) : this(sharedReader, sharedIndex, false)
			{
			}

			// Token: 0x06006D71 RID: 28017 RVA: 0x00178E92 File Offset: 0x00177092
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex, bool useSingleChunking)
			{
				this.m_sharedReader = sharedReader;
				this.m_sharedIndex = sharedIndex;
				this.m_maxChunkSize = (useSingleChunking ? 1 : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize);
			}

			// Token: 0x06006D72 RID: 28018
			protected abstract bool GrabNextChunk(int requestedChunkSize);

			// Token: 0x170012D0 RID: 4816
			// (get) Token: 0x06006D73 RID: 28019
			// (set) Token: 0x06006D74 RID: 28020
			protected abstract bool HasNoElementsLeft { get; set; }

			// Token: 0x170012D1 RID: 4817
			// (get) Token: 0x06006D75 RID: 28021
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06006D76 RID: 28022
			public abstract void Dispose();

			// Token: 0x06006D77 RID: 28023 RVA: 0x00178EB9 File Offset: 0x001770B9
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170012D2 RID: 4818
			// (get) Token: 0x06006D78 RID: 28024 RVA: 0x00178EC0 File Offset: 0x001770C0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006D79 RID: 28025 RVA: 0x00178ED0 File Offset: 0x001770D0
			public bool MoveNext()
			{
				if (this.m_localOffset == null)
				{
					this.m_localOffset = new Partitioner.SharedInt(-1);
					this.m_currentChunkSize = new Partitioner.SharedInt(0);
					this.m_doublingCountdown = 3;
				}
				if (this.m_localOffset.Value < this.m_currentChunkSize.Value - 1)
				{
					this.m_localOffset.Value++;
					return true;
				}
				int requestedChunkSize;
				if (this.m_currentChunkSize.Value == 0)
				{
					requestedChunkSize = 1;
				}
				else if (this.m_doublingCountdown > 0)
				{
					requestedChunkSize = this.m_currentChunkSize.Value;
				}
				else
				{
					requestedChunkSize = Math.Min(this.m_currentChunkSize.Value * 2, this.m_maxChunkSize);
					this.m_doublingCountdown = 3;
				}
				this.m_doublingCountdown--;
				if (this.GrabNextChunk(requestedChunkSize))
				{
					this.m_localOffset.Value = 0;
					return true;
				}
				return false;
			}

			// Token: 0x040034D0 RID: 13520
			protected readonly TSourceReader m_sharedReader;

			// Token: 0x040034D1 RID: 13521
			protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();

			// Token: 0x040034D2 RID: 13522
			protected Partitioner.SharedInt m_currentChunkSize;

			// Token: 0x040034D3 RID: 13523
			protected Partitioner.SharedInt m_localOffset;

			// Token: 0x040034D4 RID: 13524
			private const int CHUNK_DOUBLING_RATE = 3;

			// Token: 0x040034D5 RID: 13525
			private int m_doublingCountdown;

			// Token: 0x040034D6 RID: 13526
			protected readonly int m_maxChunkSize;

			// Token: 0x040034D7 RID: 13527
			protected readonly Partitioner.SharedLong m_sharedIndex;
		}

		// Token: 0x02000B96 RID: 2966
		private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006D7B RID: 28027 RVA: 0x00178FBD File Offset: 0x001771BD
			internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions) : base(true, false, true)
			{
				this.m_source = source;
				this.m_useSingleChunking = ((partitionerOptions & EnumerablePartitionerOptions.NoBuffering) > EnumerablePartitionerOptions.None);
			}

			// Token: 0x06006D7C RID: 28028 RVA: 0x00178FDC File Offset: 0x001771DC
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> enumerable = new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, true);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = enumerable.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06006D7D RID: 28029 RVA: 0x0017902D File Offset: 0x0017722D
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, false);
			}

			// Token: 0x170012D3 RID: 4819
			// (get) Token: 0x06006D7E RID: 28030 RVA: 0x00179046 File Offset: 0x00177246
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040034D8 RID: 13528
			private IEnumerable<TSource> m_source;

			// Token: 0x040034D9 RID: 13529
			private readonly bool m_useSingleChunking;

			// Token: 0x02000CCE RID: 3278
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
			{
				// Token: 0x060070CD RID: 28877 RVA: 0x00183FC0 File Offset: 0x001821C0
				internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, bool useSingleChunking, bool isStaticPartitioning)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
					this.m_hasNoElementsLeft = new Partitioner.SharedBool(false);
					this.m_sourceDepleted = new Partitioner.SharedBool(false);
					this.m_sharedLock = new object();
					this.m_useSingleChunking = useSingleChunking;
					if (!this.m_useSingleChunking)
					{
						int num = (PlatformHelper.ProcessorCount > 4) ? 4 : 1;
						this.m_FillBuffer = new KeyValuePair<long, TSource>[num * Partitioner.GetDefaultChunkSize<TSource>()];
					}
					if (isStaticPartitioning)
					{
						this.m_activePartitionCount = new Partitioner.SharedInt(0);
						return;
					}
					this.m_activePartitionCount = null;
				}

				// Token: 0x060070CE RID: 28878 RVA: 0x00184054 File Offset: 0x00182254
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					if (this.m_disposed)
					{
						throw new ObjectDisposedException(Environment.GetResourceString("PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed"));
					}
					return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex, this.m_hasNoElementsLeft, this.m_sharedLock, this.m_activePartitionCount, this, this.m_useSingleChunking);
				}

				// Token: 0x060070CF RID: 28879 RVA: 0x001840A3 File Offset: 0x001822A3
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060070D0 RID: 28880 RVA: 0x001840AC File Offset: 0x001822AC
				private void TryCopyFromFillBuffer(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
					if (fillBuffer == null)
					{
						return;
					}
					if (this.m_FillBufferCurrentPosition >= this.m_FillBufferSize)
					{
						return;
					}
					Interlocked.Increment(ref this.m_activeCopiers);
					int num = Interlocked.Add(ref this.m_FillBufferCurrentPosition, requestedChunkSize);
					int num2 = num - requestedChunkSize;
					if (num2 < this.m_FillBufferSize)
					{
						actualNumElementsGrabbed = ((num < this.m_FillBufferSize) ? num : (this.m_FillBufferSize - num2));
						Array.Copy(fillBuffer, num2, destArray, 0, actualNumElementsGrabbed);
					}
					Interlocked.Decrement(ref this.m_activeCopiers);
				}

				// Token: 0x060070D1 RID: 28881 RVA: 0x00184135 File Offset: 0x00182335
				internal bool GrabChunk(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					if (this.m_hasNoElementsLeft.Value)
					{
						return false;
					}
					if (this.m_useSingleChunking)
					{
						return this.GrabChunk_Single(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					}
					return this.GrabChunk_Buffered(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
				}

				// Token: 0x060070D2 RID: 28882 RVA: 0x00184168 File Offset: 0x00182368
				internal bool GrabChunk_Single(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					object sharedLock = this.m_sharedLock;
					bool result;
					lock (sharedLock)
					{
						if (this.m_hasNoElementsLeft.Value)
						{
							result = false;
						}
						else
						{
							try
							{
								if (this.m_sharedReader.MoveNext())
								{
									this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
									destArray[0] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
									actualNumElementsGrabbed = 1;
									result = true;
								}
								else
								{
									this.m_sourceDepleted.Value = true;
									this.m_hasNoElementsLeft.Value = true;
									result = false;
								}
							}
							catch
							{
								this.m_sourceDepleted.Value = true;
								this.m_hasNoElementsLeft.Value = true;
								throw;
							}
						}
					}
					return result;
				}

				// Token: 0x060070D3 RID: 28883 RVA: 0x00184254 File Offset: 0x00182454
				internal bool GrabChunk_Buffered(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					this.TryCopyFromFillBuffer(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					if (actualNumElementsGrabbed == requestedChunkSize)
					{
						return true;
					}
					if (this.m_sourceDepleted.Value)
					{
						this.m_hasNoElementsLeft.Value = true;
						this.m_FillBuffer = null;
						return actualNumElementsGrabbed > 0;
					}
					object sharedLock = this.m_sharedLock;
					lock (sharedLock)
					{
						if (this.m_sourceDepleted.Value)
						{
							return actualNumElementsGrabbed > 0;
						}
						try
						{
							if (this.m_activeCopiers > 0)
							{
								SpinWait spinWait = default(SpinWait);
								while (this.m_activeCopiers > 0)
								{
									spinWait.SpinOnce();
								}
							}
							while (actualNumElementsGrabbed < requestedChunkSize)
							{
								if (!this.m_sharedReader.MoveNext())
								{
									this.m_sourceDepleted.Value = true;
									break;
								}
								this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
								destArray[actualNumElementsGrabbed] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
								actualNumElementsGrabbed++;
							}
							KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
							if (!this.m_sourceDepleted.Value && fillBuffer != null && this.m_FillBufferCurrentPosition >= fillBuffer.Length)
							{
								for (int i = 0; i < fillBuffer.Length; i++)
								{
									if (!this.m_sharedReader.MoveNext())
									{
										this.m_sourceDepleted.Value = true;
										this.m_FillBufferSize = i;
										break;
									}
									this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
									fillBuffer[i] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
								}
								this.m_FillBufferCurrentPosition = 0;
							}
						}
						catch
						{
							this.m_sourceDepleted.Value = true;
							this.m_hasNoElementsLeft.Value = true;
							throw;
						}
					}
					return actualNumElementsGrabbed > 0;
				}

				// Token: 0x060070D4 RID: 28884 RVA: 0x00184470 File Offset: 0x00182670
				public void Dispose()
				{
					if (!this.m_disposed)
					{
						this.m_disposed = true;
						this.m_sharedReader.Dispose();
					}
				}

				// Token: 0x0400385B RID: 14427
				private readonly IEnumerator<TSource> m_sharedReader;

				// Token: 0x0400385C RID: 14428
				private Partitioner.SharedLong m_sharedIndex;

				// Token: 0x0400385D RID: 14429
				private volatile KeyValuePair<long, TSource>[] m_FillBuffer;

				// Token: 0x0400385E RID: 14430
				private volatile int m_FillBufferSize;

				// Token: 0x0400385F RID: 14431
				private volatile int m_FillBufferCurrentPosition;

				// Token: 0x04003860 RID: 14432
				private volatile int m_activeCopiers;

				// Token: 0x04003861 RID: 14433
				private Partitioner.SharedBool m_hasNoElementsLeft;

				// Token: 0x04003862 RID: 14434
				private Partitioner.SharedBool m_sourceDepleted;

				// Token: 0x04003863 RID: 14435
				private object m_sharedLock;

				// Token: 0x04003864 RID: 14436
				private bool m_disposed;

				// Token: 0x04003865 RID: 14437
				private Partitioner.SharedInt m_activePartitionCount;

				// Token: 0x04003866 RID: 14438
				private readonly bool m_useSingleChunking;
			}

			// Token: 0x02000CCF RID: 3279
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
			{
				// Token: 0x060070D5 RID: 28885 RVA: 0x0018448C File Offset: 0x0018268C
				internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.SharedLong sharedIndex, Partitioner.SharedBool hasNoElementsLeft, object sharedLock, Partitioner.SharedInt activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, bool useSingleChunking) : base(sharedReader, sharedIndex, useSingleChunking)
				{
					this.m_hasNoElementsLeft = hasNoElementsLeft;
					this.m_sharedLock = sharedLock;
					this.m_enumerable = enumerable;
					this.m_activePartitionCount = activePartitionCount;
					if (this.m_activePartitionCount != null)
					{
						Interlocked.Increment(ref this.m_activePartitionCount.Value);
					}
				}

				// Token: 0x060070D6 RID: 28886 RVA: 0x001844DC File Offset: 0x001826DC
				protected override bool GrabNextChunk(int requestedChunkSize)
				{
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					if (this.m_localList == null)
					{
						this.m_localList = new KeyValuePair<long, TSource>[this.m_maxChunkSize];
					}
					return this.m_enumerable.GrabChunk(this.m_localList, requestedChunkSize, ref this.m_currentChunkSize.Value);
				}

				// Token: 0x1700136B RID: 4971
				// (get) Token: 0x060070D7 RID: 28887 RVA: 0x00184529 File Offset: 0x00182729
				// (set) Token: 0x060070D8 RID: 28888 RVA: 0x00184538 File Offset: 0x00182738
				protected override bool HasNoElementsLeft
				{
					get
					{
						return this.m_hasNoElementsLeft.Value;
					}
					set
					{
						this.m_hasNoElementsLeft.Value = true;
					}
				}

				// Token: 0x1700136C RID: 4972
				// (get) Token: 0x060070D9 RID: 28889 RVA: 0x00184548 File Offset: 0x00182748
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return this.m_localList[this.m_localOffset.Value];
					}
				}

				// Token: 0x060070DA RID: 28890 RVA: 0x0018457A File Offset: 0x0018277A
				public override void Dispose()
				{
					if (this.m_activePartitionCount != null && Interlocked.Decrement(ref this.m_activePartitionCount.Value) == 0)
					{
						this.m_enumerable.Dispose();
					}
				}

				// Token: 0x04003867 RID: 14439
				private KeyValuePair<long, TSource>[] m_localList;

				// Token: 0x04003868 RID: 14440
				private readonly Partitioner.SharedBool m_hasNoElementsLeft;

				// Token: 0x04003869 RID: 14441
				private readonly object m_sharedLock;

				// Token: 0x0400386A RID: 14442
				private readonly Partitioner.SharedInt m_activePartitionCount;

				// Token: 0x0400386B RID: 14443
				private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable m_enumerable;
			}
		}

		// Token: 0x02000B97 RID: 2967
		private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006D7F RID: 28031 RVA: 0x00179049 File Offset: 0x00177249
			protected DynamicPartitionerForIndexRange_Abstract(TCollection data) : base(true, false, true)
			{
				this.m_data = data;
			}

			// Token: 0x06006D80 RID: 28032
			protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

			// Token: 0x06006D81 RID: 28033 RVA: 0x0017905C File Offset: 0x0017725C
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions_Factory = this.GetOrderableDynamicPartitions_Factory(this.m_data);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = orderableDynamicPartitions_Factory.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06006D82 RID: 28034 RVA: 0x001790A2 File Offset: 0x001772A2
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return this.GetOrderableDynamicPartitions_Factory(this.m_data);
			}

			// Token: 0x170012D4 RID: 4820
			// (get) Token: 0x06006D83 RID: 28035 RVA: 0x001790B0 File Offset: 0x001772B0
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040034DA RID: 13530
			private TCollection m_data;
		}

		// Token: 0x02000B98 RID: 2968
		private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
		{
			// Token: 0x06006D84 RID: 28036 RVA: 0x001790B3 File Offset: 0x001772B3
			protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex) : base(sharedReader, sharedIndex)
			{
			}

			// Token: 0x170012D5 RID: 4821
			// (get) Token: 0x06006D85 RID: 28037
			protected abstract int SourceCount { get; }

			// Token: 0x06006D86 RID: 28038 RVA: 0x001790C0 File Offset: 0x001772C0
			protected override bool GrabNextChunk(int requestedChunkSize)
			{
				while (!this.HasNoElementsLeft)
				{
					long num = Volatile.Read(ref this.m_sharedIndex.Value);
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					long num2 = Math.Min((long)(this.SourceCount - 1), num + (long)requestedChunkSize);
					if (Interlocked.CompareExchange(ref this.m_sharedIndex.Value, num2, num) == num)
					{
						this.m_currentChunkSize.Value = (int)(num2 - num);
						this.m_localOffset.Value = -1;
						this.m_startIndex = (int)(num + 1L);
						return true;
					}
				}
				return false;
			}

			// Token: 0x170012D6 RID: 4822
			// (get) Token: 0x06006D87 RID: 28039 RVA: 0x00179147 File Offset: 0x00177347
			// (set) Token: 0x06006D88 RID: 28040 RVA: 0x00179167 File Offset: 0x00177367
			protected override bool HasNoElementsLeft
			{
				get
				{
					return Volatile.Read(ref this.m_sharedIndex.Value) >= (long)(this.SourceCount - 1);
				}
				set
				{
				}
			}

			// Token: 0x06006D89 RID: 28041 RVA: 0x00179169 File Offset: 0x00177369
			public override void Dispose()
			{
			}

			// Token: 0x040034DB RID: 13531
			protected int m_startIndex;
		}

		// Token: 0x02000B99 RID: 2969
		private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
		{
			// Token: 0x06006D8A RID: 28042 RVA: 0x0017916B File Offset: 0x0017736B
			internal DynamicPartitionerForIList(IList<TSource> source) : base(source)
			{
			}

			// Token: 0x06006D8B RID: 28043 RVA: 0x00179174 File Offset: 0x00177374
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> m_data)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(m_data);
			}

			// Token: 0x02000CD0 RID: 3280
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x060070DB RID: 28891 RVA: 0x001845A1 File Offset: 0x001827A1
				internal InternalPartitionEnumerable(IList<TSource> sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x060070DC RID: 28892 RVA: 0x001845BD File Offset: 0x001827BD
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				// Token: 0x060070DD RID: 28893 RVA: 0x001845D0 File Offset: 0x001827D0
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0400386C RID: 14444
				private readonly IList<TSource> m_sharedReader;

				// Token: 0x0400386D RID: 14445
				private Partitioner.SharedLong m_sharedIndex;
			}

			// Token: 0x02000CD1 RID: 3281
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
			{
				// Token: 0x060070DE RID: 28894 RVA: 0x001845D8 File Offset: 0x001827D8
				internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.SharedLong sharedIndex) : base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x1700136D RID: 4973
				// (get) Token: 0x060070DF RID: 28895 RVA: 0x001845E2 File Offset: 0x001827E2
				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Count;
					}
				}

				// Token: 0x1700136E RID: 4974
				// (get) Token: 0x060070E0 RID: 28896 RVA: 0x001845F0 File Offset: 0x001827F0
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000B9A RID: 2970
		private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
		{
			// Token: 0x06006D8C RID: 28044 RVA: 0x0017917C File Offset: 0x0017737C
			internal DynamicPartitionerForArray(TSource[] source) : base(source)
			{
			}

			// Token: 0x06006D8D RID: 28045 RVA: 0x00179185 File Offset: 0x00177385
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] m_data)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(m_data);
			}

			// Token: 0x02000CD2 RID: 3282
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x060070E1 RID: 28897 RVA: 0x0018464E File Offset: 0x0018284E
				internal InternalPartitionEnumerable(TSource[] sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x060070E2 RID: 28898 RVA: 0x0018466A File Offset: 0x0018286A
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060070E3 RID: 28899 RVA: 0x00184672 File Offset: 0x00182872
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				// Token: 0x0400386E RID: 14446
				private readonly TSource[] m_sharedReader;

				// Token: 0x0400386F RID: 14447
				private Partitioner.SharedLong m_sharedIndex;
			}

			// Token: 0x02000CD3 RID: 3283
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
			{
				// Token: 0x060070E4 RID: 28900 RVA: 0x00184685 File Offset: 0x00182885
				internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.SharedLong sharedIndex) : base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x1700136F RID: 4975
				// (get) Token: 0x060070E5 RID: 28901 RVA: 0x0018468F File Offset: 0x0018288F
				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Length;
					}
				}

				// Token: 0x17001370 RID: 4976
				// (get) Token: 0x060070E6 RID: 28902 RVA: 0x0018469C File Offset: 0x0018289C
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000B9B RID: 2971
		private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006D8E RID: 28046 RVA: 0x0017918D File Offset: 0x0017738D
			protected StaticIndexRangePartitioner() : base(true, true, true)
			{
			}

			// Token: 0x170012D7 RID: 4823
			// (get) Token: 0x06006D8F RID: 28047
			protected abstract int SourceCount { get; }

			// Token: 0x06006D90 RID: 28048
			protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

			// Token: 0x06006D91 RID: 28049 RVA: 0x00179198 File Offset: 0x00177398
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				int num2;
				int num = Math.DivRem(this.SourceCount, partitionCount, out num2);
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				int num3 = -1;
				for (int i = 0; i < partitionCount; i++)
				{
					int num4 = num3 + 1;
					if (i < num2)
					{
						num3 = num4 + num;
					}
					else
					{
						num3 = num4 + num - 1;
					}
					array[i] = this.CreatePartition(num4, num3);
				}
				return array;
			}
		}

		// Token: 0x02000B9C RID: 2972
		private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06006D92 RID: 28050 RVA: 0x00179202 File Offset: 0x00177402
			protected StaticIndexRangePartition(int startIndex, int endIndex)
			{
				this.m_startIndex = startIndex;
				this.m_endIndex = endIndex;
				this.m_offset = startIndex - 1;
			}

			// Token: 0x170012D8 RID: 4824
			// (get) Token: 0x06006D93 RID: 28051
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06006D94 RID: 28052 RVA: 0x00179223 File Offset: 0x00177423
			public void Dispose()
			{
			}

			// Token: 0x06006D95 RID: 28053 RVA: 0x00179225 File Offset: 0x00177425
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006D96 RID: 28054 RVA: 0x0017922C File Offset: 0x0017742C
			public bool MoveNext()
			{
				if (this.m_offset < this.m_endIndex)
				{
					this.m_offset++;
					return true;
				}
				this.m_offset = this.m_endIndex + 1;
				return false;
			}

			// Token: 0x170012D9 RID: 4825
			// (get) Token: 0x06006D97 RID: 28055 RVA: 0x00179263 File Offset: 0x00177463
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040034DC RID: 13532
			protected readonly int m_startIndex;

			// Token: 0x040034DD RID: 13533
			protected readonly int m_endIndex;

			// Token: 0x040034DE RID: 13534
			protected volatile int m_offset;
		}

		// Token: 0x02000B9D RID: 2973
		private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
		{
			// Token: 0x06006D98 RID: 28056 RVA: 0x00179270 File Offset: 0x00177470
			internal StaticIndexRangePartitionerForIList(IList<TSource> list)
			{
				this.m_list = list;
			}

			// Token: 0x170012DA RID: 4826
			// (get) Token: 0x06006D99 RID: 28057 RVA: 0x0017927F File Offset: 0x0017747F
			protected override int SourceCount
			{
				get
				{
					return this.m_list.Count;
				}
			}

			// Token: 0x06006D9A RID: 28058 RVA: 0x0017928C File Offset: 0x0017748C
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForIList<TSource>(this.m_list, startIndex, endIndex);
			}

			// Token: 0x040034DF RID: 13535
			private IList<TSource> m_list;
		}

		// Token: 0x02000B9E RID: 2974
		private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06006D9B RID: 28059 RVA: 0x0017929B File Offset: 0x0017749B
			internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex) : base(startIndex, endIndex)
			{
				this.m_list = list;
			}

			// Token: 0x170012DB RID: 4827
			// (get) Token: 0x06006D9C RID: 28060 RVA: 0x001792B0 File Offset: 0x001774B0
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_list[this.m_offset]);
				}
			}

			// Token: 0x040034E0 RID: 13536
			private volatile IList<TSource> m_list;
		}

		// Token: 0x02000B9F RID: 2975
		private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
		{
			// Token: 0x06006D9D RID: 28061 RVA: 0x00179300 File Offset: 0x00177500
			internal StaticIndexRangePartitionerForArray(TSource[] array)
			{
				this.m_array = array;
			}

			// Token: 0x170012DC RID: 4828
			// (get) Token: 0x06006D9E RID: 28062 RVA: 0x0017930F File Offset: 0x0017750F
			protected override int SourceCount
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x06006D9F RID: 28063 RVA: 0x00179319 File Offset: 0x00177519
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForArray<TSource>(this.m_array, startIndex, endIndex);
			}

			// Token: 0x040034E1 RID: 13537
			private TSource[] m_array;
		}

		// Token: 0x02000BA0 RID: 2976
		private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06006DA0 RID: 28064 RVA: 0x00179328 File Offset: 0x00177528
			internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex) : base(startIndex, endIndex)
			{
				this.m_array = array;
			}

			// Token: 0x170012DD RID: 4829
			// (get) Token: 0x06006DA1 RID: 28065 RVA: 0x0017933C File Offset: 0x0017753C
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_array[this.m_offset]);
				}
			}

			// Token: 0x040034E2 RID: 13538
			private volatile TSource[] m_array;
		}

		// Token: 0x02000BA1 RID: 2977
		private class SharedInt
		{
			// Token: 0x06006DA2 RID: 28066 RVA: 0x0017938C File Offset: 0x0017758C
			internal SharedInt(int value)
			{
				this.Value = value;
			}

			// Token: 0x040034E3 RID: 13539
			internal volatile int Value;
		}

		// Token: 0x02000BA2 RID: 2978
		private class SharedBool
		{
			// Token: 0x06006DA3 RID: 28067 RVA: 0x0017939D File Offset: 0x0017759D
			internal SharedBool(bool value)
			{
				this.Value = value;
			}

			// Token: 0x040034E4 RID: 13540
			internal volatile bool Value;
		}

		// Token: 0x02000BA3 RID: 2979
		private class SharedLong
		{
			// Token: 0x06006DA4 RID: 28068 RVA: 0x001793AE File Offset: 0x001775AE
			internal SharedLong(long value)
			{
				this.Value = value;
			}

			// Token: 0x040034E5 RID: 13541
			internal long Value;
		}
	}
}
