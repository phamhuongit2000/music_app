import { View, Text, StyleSheet, ScrollView, Image } from 'react-native';
import { RouteProp } from '@react-navigation/native';
import React, { useEffect, useState } from 'react';
import { getSingerId, SERVER_DOMAIN } from '../config';
import AudioPlayer from '@/components/AudioPlayer';

type AlbumDetailsScreenRouteProp = RouteProp<{ AlbumDetails: { album: string } }, 'AlbumDetails'>;

type Props = {
  route: AlbumDetailsScreenRouteProp;
};

const SingerDetailsScreen: React.FC<Props> = ({ route }) => {
    const singerId = getSingerId();
    const [songsData, setSongsData] = useState<MusicItem[]>([]);
    const [singersData, setSingerData] = useState<SingerItem>();
    useEffect(() => {
        const fetchSingerData = async () => {
            try {
                const response = await fetch(`${SERVER_DOMAIN}/api/Singer/GetById/${singerId}`);
                const json = await response.json();
                setSingerData(json);
                setSongsData(json.listSongsInfo);
            } catch (error) {
                console.error('Error fetching music data:', error);
            }
        };

        fetchSingerData();
    }, []);

  return (
    <View style={styles.container}>
        <Image source={{ uri: singersData?.avatarUrl }} style={styles.albumImage} />
        <Text style={styles.albumTitle}>{singersData?.name}</Text>
      <ScrollView style={styles.horizontalScrollView}>
        {songsData.map((item, index) => (
          <View key={index} style={[
            styles.musicItemContainer,
            { backgroundColor: index % 2 === 0 ? '#f0f0f0' : '#ffffff' } // Alternating colors
          ]}>
            <AudioPlayer
              url={item.audioUrl}
              img={item.imgUrl}
              name_music={item.name}
              name_singer={item.singerName}
              style={{ width: 100, height: 100, marginRight: 10 }}
            />
            <View style={styles.songInfo}>
              <Text numberOfLines={1} style={styles.newMusicSinger}>{item.name}</Text>
              <Text numberOfLines={1} style={styles.newMusicName}>{item.singerName}</Text>
            </View>
            <View style={styles.likeContainer}>
              <Image
                source={
                  require('../assets/heart_enable.png')
                }
                style={styles.likeButton}
              />
              <Text style={styles.likeText}>
                {
                  parseInt(item.views) >= 1000 && parseInt(item.views) < 100000
                  ? `${(parseInt(item.views) / 1000).toFixed(1)}N`
                  : parseInt(item.views) >= 100000 && parseInt(item.views) < 1000000000
                  ? `${(parseInt(item.views) / 1000000).toFixed(1)}M`
                  : parseInt(item.views) >= 1000000000
                  ? `${(parseInt(item.views) / 1000000000).toFixed(1)}T`
                  : item.views
                }
              </Text>
            </View>
          </View>
        ))}
      </ScrollView>
    </View>
  );
};

export default SingerDetailsScreen;


const styles = StyleSheet.create({
    container: {
      alignItems: "center",
      padding: 10,
      flex: 1,
      backgroundColor: 'skyblue',
    },
    albumImage: {
        width: 200,
        height: 200,
        resizeMode: "cover",
        marginBottom: 10,
        borderRadius: 100,
    },
    albumTitle: {
        textAlign: "center",
        fontSize: 24,
        fontWeight: 'bold',
        marginBottom: 10,
    },
    musicItemContainer: {
      flexDirection: 'row',
      alignItems: 'center',
      marginBottom: 15,
      padding: 10,
      borderRadius: 10
    },
    songInfo: {
      width: 140,
      flexDirection: 'column',
      textAlign: 'center',
      marginBottom: 10,
      paddingHorizontal: 10,
    },
    title: {
      fontSize: 24,
      height: 50,
      fontWeight: 'bold',
    },
    coverImageSubtitle: {
      fontSize: 16,
      left: 20,
    },
    newMusicSinger: {
      fontWeight: 'bold',
    },
    newMusicName: {
    },
    horizontalScrollView: {
  
    },
    likeContainer: {
      flexDirection: 'row',
      alignItems: 'center'
    },
    likeButton: {
      width: 24,
      height: 24,
      marginRight: 5,
      tintColor: 'red'
    },
    likeText: {
      fontSize: 16,
    }
  });

interface MusicItem {
    id: string;
    audioUrl: string;
    imgUrl: string;
    name: string;
    singerName: string;
    views: string;
    likes: string;
}

interface AlbumItem {
    id: string;
    name: string;
    description: string;
    imgUrl: string;
    listSongsInfo: MusicItem[];
}

interface SingerItem {
    id: string;
    name: string;
    description: string;
    avatarUrl: string;
}
  