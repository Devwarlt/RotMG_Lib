/*  1:   */ package realmrelay.packets.client;
/*  2:   */ 
/*  3:   */ import java.io.DataInput;
/*  4:   */ import java.io.DataOutput;
/*  5:   */ import java.io.IOException;
/*  6:   */ import realmrelay.data.Location;
/*  7:   */ import realmrelay.packets.Packet;
/*  8:   */ 
/*  9:   */ public class PlayerShootPacket
/* 10:   */   extends Packet
/* 11:   */ {
/* 12:   */   public int time;
/* 13:   */   public int bulletId;
/* 14:   */   public int containerType;
/* 15:16 */   public Location startingPos = new Location();
/* 16:   */   public float angle;
/* 17:   */   
/* 18:   */   public void parseFromInput(DataInput in)
/* 19:   */     throws IOException
/* 20:   */   {
/* 21:21 */     this.time = in.readInt();
/* 22:22 */     this.bulletId = in.readUnsignedByte();
/* 23:23 */     this.containerType = in.readUnsignedShort();
/* 24:24 */     this.startingPos.parseFromInput(in);
/* 25:25 */     this.angle = in.readFloat();
/* 26:   */   }
/* 27:   */   
/* 28:   */   public void writeToOutput(DataOutput out)
/* 29:   */     throws IOException
/* 30:   */   {
/* 31:30 */     out.writeInt(this.time);
/* 32:31 */     out.writeByte(this.bulletId);
/* 33:32 */     out.writeShort(this.containerType);
/* 34:33 */     this.startingPos.writeToOutput(out);
/* 35:34 */     out.writeFloat(this.angle);
/* 36:   */   }
/* 37:   */ }


/* Location:           C:\Users\Fabian\Desktop\RR_GUI\RRv2.5.1.jar
 * Qualified Name:     realmrelay.packets.client.PlayerShootPacket
 * JD-Core Version:    0.7.0.1
 */