/*  1:   */ package realmrelay.packets.client;
/*  2:   */ 
/*  3:   */ import java.io.DataInput;
/*  4:   */ import java.io.DataOutput;
/*  5:   */ import java.io.IOException;
/*  6:   */ import realmrelay.packets.Packet;
/*  7:   */ 
/*  8:   */ public class SetConditionPacket
/*  9:   */   extends Packet
/* 10:   */ {
/* 11:   */   public int conditionEffect;
/* 12:   */   public float conditionDuration;
/* 13:   */   
/* 14:   */   public void parseFromInput(DataInput in)
/* 15:   */     throws IOException
/* 16:   */   {
/* 17:17 */     this.conditionEffect = in.readUnsignedByte();
/* 18:18 */     this.conditionDuration = in.readFloat();
/* 19:   */   }
/* 20:   */   
/* 21:   */   public void writeToOutput(DataOutput out)
/* 22:   */     throws IOException
/* 23:   */   {
/* 24:23 */     out.writeByte(this.conditionEffect);
/* 25:24 */     out.writeFloat(this.conditionDuration);
/* 26:   */   }
/* 27:   */ }



/* Location:           C:\Users\Fabian\Desktop\RR_GUI\RRv2.5.1.jar

 * Qualified Name:     realmrelay.packets.client.SetConditionPacket

 * JD-Core Version:    0.7.0.1

 */