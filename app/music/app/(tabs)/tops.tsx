import React, { useState, useEffect } from 'react';
import { StyleSheet, View, Text, Image, ScrollView, TouchableOpacity } from 'react-native';
import AudioPlayer from '@/components/AudioPlayer';
import { SERVER_DOMAIN } from '../../config';

export default function TopsScreen() {
  const [musicData, setMusicData] = useState<MusicItem[]>([]);
  const [selectedTrack, setSelectedTrack] = useState<MusicItem | null>(null);

  useEffect(() => {
    const fetchMusicData = async () => {
      try {
        const response = await fetch(`${SERVER_DOMAIN}/api/Song/GetAll`);
        const json = await response.json();
        setMusicData(json);
      } catch (error) {
        console.error('Error fetching music data:', error);
      }
    };

    fetchMusicData();
  }, []);

  const handleTrackPress = (item: MusicItem) => {
    setSelectedTrack(item);
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Bảng xếp hạng</Text>
      <ScrollView style={styles.horizontalScrollView}>
        {musicData.map((item, index) => (
          <View key={item._id} style={[
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
                  require('../../assets/heart_enable.png')
                }
                style={styles.likeButton}
              />
              <Text style={styles.likeText}>
                {
                  parseInt(item.likes) >= 1000 && parseInt(item.likes) < 100000
                  ? `${(parseInt(item.likes) / 1000).toFixed(1)}N`
                  : parseInt(item.likes) >= 100000 && parseInt(item.likes) < 1000000000
                  ? `${(parseInt(item.likes) / 1000000).toFixed(1)}M`
                  : parseInt(item.likes) >= 1000000000
                  ? `${(parseInt(item.likes) / 1000000000).toFixed(1)}T`
                  : item.likes
                }
              </Text>
            </View>
          </View>
        ))}
      </ScrollView>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    padding: 10,
    flex: 1,
    backgroundColor: 'skyblue',
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
  _id: string;
  audioUrl: string;
  imgUrl: string;
  name: string;
  singerName: string;
  view: string;
  likes: string;
}
