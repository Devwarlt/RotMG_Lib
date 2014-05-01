/*  1:   */ package realmrelay.packets.client;
/*  2:   */ 
/*  3:   */ import java.io.DataInput;
/*  4:   */ import java.io.DataOutput;
/*  5:   */ import java.io.IOException;
/*  6:   */ import realmrelay.data.Location;
/*  7:   */ import realmrelay.data.SlotObject;
/*  8:   */ import realmrelay.packets.Packet;
/*  9:   */ 
/* 10:   */ public class UseItemPacket
/* 11:   */   extends Packet
/* 12:   */ {
/* 13:   */   public int time;
/* 14:15 */   public SlotObject slotObject = new SlotObject();
/* 15:16 */   public Location itemUsePos = new Location();
/* 16:   */   public int useType;
/* 17:   */   
/* 18:   */   public void parseFromInput(DataInput in)
/* 19:   */     throws IOException
/* 20:   */   {
/* 21:21 */     this.time = in.readInt();
/* 22:22 */     this.slotObject.parseFromInput(in);
/* 23:23 */     this.itemUsePos.parseFromInput(in);
/* 24:24 */     this.useType = in.readUnsignedByte();
/* 25:   */   }
/* 26:   */   
/* 27:   */   public void writeToOutput(DataOutput out)
/* 28:   */     throws IOException
/* 29:   */   {
/* 30:29 */     out.writeInt(this.time);
/* 31:30 */     this.slotObject.writeToOutput(out);
/* 32:31 */     this.itemUsePos.writeToOutput(out);
/* 33:32 */     out.writeByte(this.useType);
/* 34:   */   }
/* 35:   */ }


/* Location:           C:\Users\Fabian\Desktop\RR_GUI\RRv2.5.1.jar
 * Qualified Name:     realmrelay.packets.client.UseItemPacket
 * JD-Core Version:    0.7.0.1
 */