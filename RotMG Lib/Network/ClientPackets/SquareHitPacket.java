/*  1:   */ package realmrelay.packets.client;
/*  2:   */ 
/*  3:   */ import java.io.DataInput;
/*  4:   */ import java.io.DataOutput;
/*  5:   */ import java.io.IOException;
/*  6:   */ import realmrelay.packets.Packet;
/*  7:   */ 
/*  8:   */ public class SquareHitPacket
/*  9:   */   extends Packet
/* 10:   */ {
/* 11:   */   public int time;
/* 12:   */   public int bulletId;
/* 13:   */   public int objectId;
/* 14:   */   
/* 15:   */   public void parseFromInput(DataInput in)
/* 16:   */     throws IOException
/* 17:   */   {
/* 18:18 */     this.time = in.readInt();
/* 19:19 */     this.bulletId = in.readUnsignedByte();
/* 20:20 */     this.objectId = in.readInt();
/* 21:   */   }
/* 22:   */   
/* 23:   */   public void writeToOutput(DataOutput out)
/* 24:   */     throws IOException
/* 25:   */   {
/* 26:25 */     out.writeInt(this.time);
/* 27:26 */     out.writeByte(this.bulletId);
/* 28:27 */     out.writeInt(this.objectId);
/* 29:   */   }
/* 30:   */ }


/* Location:           C:\Users\Fabian\Desktop\RR_GUI\RRv2.5.1.jar
 * Qualified Name:     realmrelay.packets.client.SquareHitPacket
 * JD-Core Version:    0.7.0.1
 */