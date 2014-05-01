/*  1:   */ package realmrelay.packets.client;
/*  2:   */ 
/*  3:   */ import java.io.DataInput;
/*  4:   */ import java.io.DataOutput;
/*  5:   */ import java.io.IOException;
/*  6:   */ import realmrelay.packets.Packet;
/*  7:   */ 
/*  8:   */ public class ShootAckPacket
/*  9:   */   extends Packet
/* 10:   */ {
/* 11:   */   public int time;
/* 12:   */   
/* 13:   */   public void parseFromInput(DataInput in)
/* 14:   */     throws IOException
/* 15:   */   {
/* 16:16 */     this.time = in.readInt();
/* 17:   */   }
/* 18:   */   
/* 19:   */   public void writeToOutput(DataOutput out)
/* 20:   */     throws IOException
/* 21:   */   {
/* 22:21 */     out.writeInt(this.time);
/* 23:   */   }
/* 24:   */ }


/* Location:           C:\Users\Fabian\Desktop\RR_GUI\RRv2.5.1.jar
 * Qualified Name:     realmrelay.packets.client.ShootAckPacket
 * JD-Core Version:    0.7.0.1
 */